using Microsoft.EntityFrameworkCore;
using WebApiFinal.Data;
using WebApiFinal.Interfaces;
using WebApiFinal.Models;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApiFinal.Repositories
{
    //  guarda usuario con hash 
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context) => _context = context;

        public async Task<Usuario?> ValidateUserAsync(string email, string password)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;

            // Verificar hash 
            var hash = ComputeHash(password);
            return user.PasswordHash == hash ? user : null;
        }

        public async Task<Usuario> CreateUserAsync(string email, string password)
        {
            var nuevo = new Usuario
            {
                Email = email,
                PasswordHash = ComputeHash(password),
                Rol = "User"
            };
            _context.Usuarios.Add(nuevo);
            await _context.SaveChangesAsync();
            return nuevo;
        }

        private string ComputeHash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
