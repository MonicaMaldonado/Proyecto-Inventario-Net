using System.Threading.Tasks;

namespace TransaccionApi.Repository
{
    public interface ITransaccionRepository
    {
        Task<Transaccion> CrearAsync(Transaccion transaccion, CancellationToken cancellationToken = default);
        Task<IEnumerable<Transaccion>> GetByProductoIdAsync(int productoId, DateTime? fechaInicio, DateTime? fechaFin, string tipo, CancellationToken cancellationToken = default);
    }
}
