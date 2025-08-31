namespace ScraperService.Models;

public class Evento
{
    public Evento(string nombre, string url, string descripcion, DateTime fechaInicio, DateTime fechaFin, string ubicacion, CategoriaEvento categoria, ModalidadEvento modalidad, decimal costoEntrada, string fuente)
    {
        Nombre = nombre;
        URL = url;
        Descripcion = descripcion;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        Ubicacion = ubicacion;
        Categoria = categoria;
        Modalidad = modalidad;
        CostoEntrada = costoEntrada;
        Fuente = fuente;
    }

    public string Nombre { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string Ubicacion { get; set; }
    public string Descripcion { get; set; }
    public string URL { get; set; }
    public CategoriaEvento Categoria { get; set; }
    public ModalidadEvento Modalidad { get; set; }
    public decimal CostoEntrada { get; set; }
    public string Fuente { get; set; } = "Endeavor";
}