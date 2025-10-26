
using Microsoft.EntityFrameworkCore;

namespace TransaccionApi.Repository;

public class TransaccionRepository : ITransaccionRepository
{
    private readonly ApplicationDbContext _context;
    public TransaccionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Transaccion> CrearAsync(Transaccion transaccion, CancellationToken cancellationToken)
    {
        _context.Transacciones.Add(transaccion);
        await _context.SaveChangesAsync();
        return transaccion;
    }

    public async Task<IEnumerable<Transaccion>> GetByProductoIdAsync(int productoId, DateTime? fechaInicio, DateTime? fechaFin, string tipo, CancellationToken cancellationToken)
    {
        var query = _context.Transacciones
        .Where(t => t.ProductoID == productoId);

        if (fechaInicio.HasValue)
        {
            query = query.Where(t => t.Fecha.Date >= fechaInicio.Value.Date);
        }
        if (fechaFin.HasValue)
        {
            query = query.Where(t => t.Fecha.Date <= fechaFin.Value.Date);
        }
        if (!string.IsNullOrEmpty(tipo))
        {
            query = query.Where(t => t.Tipo == tipo);
        }

        return await query.OrderByDescending(t => t.Fecha).ToListAsync();
    }

}
