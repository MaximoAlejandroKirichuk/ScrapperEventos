using ScraperService;
using ScraperService.Scrapers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHttpClient<EndeavorScraper>(); // Registramos el HttpClient para EndeavorScraper
        services.AddHostedService<Worker>();

        // Cuando tengas el repositorio:
        // services.AddScoped<IEventoRepository, EventoRepository>();
    })
    .Build();

await host.RunAsync();