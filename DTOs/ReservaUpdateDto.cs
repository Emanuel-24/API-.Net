using System.ComponentModel.DataAnnotations;

namespace WebApiFinal.DTOs
{
    public class ReservaUpdateDto
    {
        [Required]
        public string Estado { get; set; } = null!; // Pendiente/Confirmada/Cancelada

        public string? Observaciones { get; set; }
    }
}
