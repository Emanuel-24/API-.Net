using System.ComponentModel.DataAnnotations;

namespace WebApiFinal.DTOs
{
    public class ClienteCreateDto
    {
        [Required, StringLength(100)]
        public string Nombre { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string? Telefono { get; set; }
    }
}
