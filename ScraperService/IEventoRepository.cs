using ScraperService.Models;

namespace ScraperService.ScraperService;

public interface IEventoRepository
{
    Task GuardarEventosAsync(List<Evento> eventos);
}