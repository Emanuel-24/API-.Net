

namespace WebApiFinal.Models
{
    // Representa la tabla Reservas
    public class Reserva
    {
        public int ReservaId { get; set; }               // PK
        public int ClienteId { get; set; }               // FK
        public Cliente Cliente { get; set; } = null!;    // navegación
        public int ServicioId { get; set; }              // FK
        public Servicio Servicio { get; set; } = null!;  // navegación

        public DateTime FechaReserva { get; set; } = DateTime.UtcNow;
        public string Estado { get; set; } = "Pendiente"; // Pendiente/Confirmada/Cancelada
        public string? Observaciones { get; set; }

        // Relación muchos a muchos
        public ICollection<ServicioReserva> ServiciosReservas { get; set; }
    }
}
