using System.Text.Json;
using ScraperService.Models;
using ScraperService.ScraperService;

namespace ScraperService.Infrastructure;

public class JsonEventoRepository : IEventoRepository
{
    private readonly string _rutaArchivo;

    public JsonEventoRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;
    }

    public async Task GuardarEventosAsync(List<Evento> eventos)
    {
        var json = JsonSerializer.Serialize(eventos, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_rutaArchivo, json);
    }
}

