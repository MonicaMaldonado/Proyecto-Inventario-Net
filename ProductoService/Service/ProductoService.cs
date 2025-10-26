using ProductoApi.Service;
using ProductoApi.Interface;
using ProductoApi.Repository;

namespace ProductoApi.Service
{
    public class ProductoService : IProductoService
    {
        public readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<Producto> CreateProductoAsync(ProductoDto dto, CancellationToken cancellationToken = default)
        {
            var productoCreado = await _productoRepository.CreateProductoAsync(dto, cancellationToken);
            return productoCreado;
        }


        public async Task<List<ProductoDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            
            var producto = await _productoRepository.GetAllAsync(cancellationToken);

            var productoDtos = producto.Select(p => new ProductoDto
            {
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Categoria = p.Categoria,
                Precio = p.Precio,
                Stock = p.Stock,
                Imagen = p.Imagen
            }).ToList();

            return productoDtos;
        }

        public async Task<bool> UpdateProductoAsync(int id, ProductoDto dto, CancellationToken cancellationToken)
        {
            var exito = await _productoRepository.UpdateProductoAsync(id, dto, cancellationToken);
            return exito;
        }


        public async Task<bool> DeleteProductoAsync(int id, CancellationToken cancellationToken = default)
        {
            var exito = await _productoRepository.DeleteProductoAsync(id, cancellationToken);
            return exito;
        }

        public async Task<bool> AjustarStockAsync(int id, int cantidadAjuste, CancellationToken cancellationToken)
        {
            return await _productoRepository.AjustarStockAsync(id, cantidadAjuste, cancellationToken); throw new NotImplementedException();
        }

        public async Task<ProductoDto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto == null)
            {
                return null; 
            }

            return new ProductoDto
            {
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Categoria = producto.Categoria,
                Precio = producto.Precio,
                Stock = producto.Stock, // <-- El stock que necesitamos
                Imagen = producto.Imagen
            };
        }
    }
}
