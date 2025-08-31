using HtmlAgilityPack;
using ScraperService.Models;

namespace ScraperService.Scrapers;

public class EndeavorScraper
{
    private readonly HttpClient _httpClient;

    public EndeavorScraper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<string>> GetUrlEventosAsync()
    {
        var url = "https://endeavor.org.ar/eventos/";
        HttpClient httpClient = _httpClient;
        var html = await httpClient.GetStringAsync(url);
        HtmlDocument document = new HtmlDocument();
        document.LoadHtml(html);

        var links = new List<string>();
        var urls = document.DocumentNode
            .SelectNodes("//article//a[@href]")
            ?.Select(a => a.GetAttributeValue("href", ""))
            .Where(href => !string.IsNullOrWhiteSpace(href))
            .Select(href => href.StartsWith("http") ? href : "https://endeavor.org.ar" + href)
            .Distinct()  // Eliminar duplicados
            .ToList();

        return urls ?? new List<string>();
    }

    public async Task<string?> GetLinkInscripcionAsync(string url)
    {
        var html = await _httpClient.GetStringAsync(url);
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        // Selector por clase de botón
        var linkNode = doc.DocumentNode
            .SelectSingleNode("//a[contains(@class,'ohio-widget') and contains(@href,'event')]");

        return linkNode?.GetAttributeValue("href", null) ?? "No encontrado";
    }



    public async Task<List<Evento>> GetEventosAsync(string url)
    {
        var eventos = new List<Evento>();


        var html = await _httpClient.GetStringAsync(url);

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        // Cada evento está en un <article>
        var nodes = doc.DocumentNode.SelectNodes("//article[contains(@class,'elementor-post')]");
        if (nodes != null)
        {
            foreach (var node in nodes)
            {
                var nombreNode = node.SelectSingleNode(".//h3[@class='elementor-post__title']/a");
                var descNode = node.SelectSingleNode(".//div[@class='elementor-post__excerpt']/p");

                var evento = new Evento(nombre: nombreNode?.InnerText.Trim() ?? "Sin nombre",
                    url: nombreNode?.GetAttributeValue("href", string.Empty) ?? string.Empty,
                    descripcion: descNode?.InnerText.Trim() ?? string.Empty,
                    fechaInicio: DateTime.MinValue, // Lo veremos en el detalle
                    fechaFin: DateTime.MinValue, ubicacion: "Desconocida", // Lo veremos en el detalle
                    categoria: CategoriaEvento.Otro, modalidad: ModalidadEvento.Presencial, costoEntrada: 0,
                    fuente: "Endeavor");

                eventos.Add(evento);
            }
        }

        return eventos;
    }
}