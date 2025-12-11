using System.ComponentModel.DataAnnotations;

namespace WebApiFinal.DTOs
{
    public class ServicioCreateDto
    {
        [Required]
        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Precio { get; set; }
    }
}
