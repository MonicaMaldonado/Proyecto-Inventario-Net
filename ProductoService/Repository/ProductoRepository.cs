
using Microsoft.EntityFrameworkCore;


namespace ProductoApi.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductoRepository(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task<IReadOnlyList<ProductoDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Productos.AsNoTracking()
                   .Select(p => new ProductoDto
                   {
                       Nombre = p.Nombre,
                       Descripcion = p.Descripcion,
                       Categoria = p.Categoria,
                       Imagen = p.Imagen,
                       Precio = p.Precio,
                       Stock = p.Stock
                   })
                  .ToListAsync(cancellationToken);
        }


        public async Task<Producto> CreateProductoAsync(ProductoDto dto, CancellationToken cancellationToken)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Categoria = dto.Categoria,
                Precio = dto.Precio,
                Imagen = dto.Imagen,   
                Stock = dto.Stock
            };

            _context.Productos.Add(producto);

            await _context.SaveChangesAsync();

            return producto;
        }

        
        public async Task<bool> UpdateProductoAsync(int id, ProductoDto dto, CancellationToken cancellationToken)
        {
            var productoExistente = await _context.Productos.FindAsync(id);

            if (productoExistente == null)
            {
                return false;
            }

            productoExistente.Nombre = dto.Nombre;
            productoExistente.Descripcion = dto.Descripcion;
            productoExistente.Categoria = dto.Categoria;
            productoExistente.Precio = dto.Precio;
            productoExistente.Imagen = dto.Imagen; 

            _context.Productos.Update(productoExistente);

            await _context.SaveChangesAsync();

            return true;
        }

        
        public async Task<bool> DeleteProductoAsync(int id, CancellationToken cancellationToken)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return false;
            }

            _context.Productos.Remove(producto);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AjustarStockAsync(int id, int cantidadAjuste, CancellationToken cancellationToken)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return false; 
            }


            if (producto.Stock + cantidadAjuste < 0)
            {
                throw new Exception("Stock insuficiente para realizar la transacción.");
            }

            producto.Stock += cantidadAjuste;

            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Producto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Productos.FindAsync(id);
        }
    }



}

