using WebApiFinal.Models;

public class Reserva
{
    public int ReservaId { get; set; }

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public DateTime FechaReserva { get; set; } = DateTime.UtcNow;
    public string Estado { get; set; } = "Pendiente";
    public string? Observaciones { get; set; }

    // Many-to-many real (correcto)
    public ICollection<ServicioReserva> ServiciosReservas { get; set; } = new List<ServicioReserva>();
}
