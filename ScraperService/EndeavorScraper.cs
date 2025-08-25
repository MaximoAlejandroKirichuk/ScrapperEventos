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

    public async Task<List<Evento>> GetEventosAsync()
    {
        var eventos = new List<Evento>();

        var url = "https://endeavor.org.ar/eventos/";
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

                var evento = new Evento
                {
                    Nombre = nombreNode?.InnerText.Trim() ?? "Sin nombre",
                    URL = nombreNode?.GetAttributeValue("href", string.Empty) ?? string.Empty,
                    Descripcion = descNode?.InnerText.Trim() ?? string.Empty,
                    FechaInicio = DateTime.MinValue, // Lo veremos en el detalle
                    FechaFin = DateTime.MinValue,
                    Ubicacion = "Desconocida",        // Lo veremos en el detalle
                    Categoria = CategoriaEvento.Otro,
                    Modalidad = ModalidadEvento.Presencial,
                    CostoEntrada = 0,
                    Fuente = "Endeavor"
                };

                eventos.Add(evento);
            }
        }

        return eventos;
    }
}