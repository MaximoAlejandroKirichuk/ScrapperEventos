using ScraperService.Scrapers;

namespace ScraperService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly EndeavorScraper _scraper;

    public Worker(ILogger<Worker> logger, EndeavorScraper scraper)
    {
        _logger = logger;
        _scraper = scraper;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Scraping eventos Endeavor... {time}", DateTimeOffset.Now);

            
            var listaUrls = await _scraper.GetUrlEventosAsync();
            
            foreach (var url in listaUrls)
            {
                Console.WriteLine("url a buscar mas info " + url); 
                var linkDescripcion = await _scraper.GetLinkInscripcionAsync(url);
                Console.WriteLine("linkDescripcion: " + linkDescripcion);
            }
            
            //foreach (var e in eventos)
            //{
              //_logger.LogInformation("Evento encontrado: {nombre} - {fecha}", e.Nombre, e.FechaInicio);
            //}

            // Acá guardar en base de datos cuando implementes DAL
            //TODO:
            // _eventoRepository.Save(eventos);

            await Task.Delay(TimeSpan.FromHours(6), stoppingToken);
        }
    }
}