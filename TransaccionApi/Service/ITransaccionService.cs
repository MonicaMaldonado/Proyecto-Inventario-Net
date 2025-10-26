namespace TransaccionApi.Service
{
    public interface ITransaccionService
    {
        Task<Transaccion> RegistrarTransaccionAsync(TransaccionDto dto, CancellationToken cancellationToken = default);
        Task<HistorialProductoDto> GetHistorialProductoAsync(int productoId, DateTime? fechaInicio, DateTime? fechaFin, string tipo, CancellationToken cancellationToken = default);
    }
}
