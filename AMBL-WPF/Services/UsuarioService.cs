using AMBL_WPF.Data;
using AMBL_WPF.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AMBL_WPF.Services
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService()
        {
            _context = new AppDbContext();
        }

        // Registrar usuario
        public async Task RegistrarUsuario(Usuario usuario)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_RegistrarUsuario @p0, @p1, @p2",
                usuario.Name,
                usuario.Password,
                usuario.Email
            );
        }

        // Login
        public async Task<Usuario?> Login(string name, string password)
        {
            var result = await _context
                .Set<Usuario>()
                .FromSqlRaw("EXEC sp_Login @p0, @p1", name, password)
                .AsNoTracking()
                .ToListAsync();

            return result.FirstOrDefault();
        }
    }
}
