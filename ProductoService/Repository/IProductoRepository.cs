namespace ProductoApi.Repository
{
    public interface IProductoRepository
    {
        Task<IReadOnlyList<ProductoDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Producto> CreateProductoAsync(ProductoDto dto, CancellationToken cancellationToken = default);
        Task<bool> UpdateProductoAsync(int id, ProductoDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteProductoAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> AjustarStockAsync(int id, int cantidadAjuste, CancellationToken cancellationToken = default);
        Task<Producto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
