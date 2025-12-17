using AMBL_WPF.Data;
using AMBL_WPF.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AMBL_WPF.Services
{
    public class ProductoService
    {
        private readonly AppDbContext _context;

        public ProductoService()
        {
            _context = new AppDbContext();
        }

        public async Task<List<Producto>> Listar()
        {
            return await _context
                .Set<Producto>()
                .FromSqlRaw("EXEC sp_ListarProductos")
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task Agregar(Producto p)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_InsertarProducto @p0, @p1, @p2",
                p.Nombre,
                p.Precio,
                p.Stock,
                p.FechaCreacion,
                p.FechaModificacion
            );
        }

        public async Task Actualizar(Producto p)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_ActualizarProducto @p0, @p1, @p2, @p3",
                p.Id,
                p.Nombre,
                p.Precio,
                p.Stock,
                p.FechaCreacion,
                p.FechaModificacion
            );
        }

        public async Task Eliminar(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_EliminarProducto @p0",
                id
            );
        }

        public async Task<List<Producto>> Buscar(string nombre)
        {
            return await _context
                .Set<Producto>()
                .FromSqlRaw("EXEC sp_BuscarProductos @p0", nombre)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
