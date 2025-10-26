using TransaccionApi;

namespace ProductoApi.Interface
{
    public interface IProductoService
    {
        Task<List<ProductoDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Producto> CreateProductoAsync(ProductoDto dto, CancellationToken cancellationToken = default);

        Task<bool> UpdateProductoAsync(int id, ProductoDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteProductoAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> AjustarStockAsync(int id, int cantidadAjuste, CancellationToken cancellationToken = default);
        Task<ProductoDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
