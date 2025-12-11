namespace WebApiFinal.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Rol { get; set; } = "User";
    }
}
