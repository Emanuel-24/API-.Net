using System.ComponentModel.DataAnnotations;

namespace WebApiFinal.DTOs
{
    public class ReservaCreateDto
    {
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public int ServicioId { get; set; }
        public DateTime FechaReserva { get; set; } = DateTime.UtcNow;
        public string? Observaciones { get; set; }
        public List<int> ServicioIds { get; set; } = new();

    }
}
