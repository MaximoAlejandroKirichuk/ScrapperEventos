using ScraperService.Models;
namespace ScraperService.ScraperService;

public interface IEventoScraper
{
    Task<List<Evento>> ScrappearEventosAsync();
}