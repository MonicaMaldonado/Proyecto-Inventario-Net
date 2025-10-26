using System.Net.Http;
using TransaccionApi.Repository;
using System.Net.Http;       
using System.Net.Http.Json;  

namespace TransaccionApi.Service
{
    public class TransaccionService : ITransaccionService
    {
        private readonly ITransaccionRepository _repo;
        private readonly HttpClient _httpClient;
        private readonly string _productoServiceUrl;

        public TransaccionService(ITransaccionRepository repo, IConfiguration configuration)
        {
            _repo = repo;
            _productoServiceUrl = configuration["ServiceUrls:ProductoApi"] ??
                              throw new ArgumentNullException("ServiceUrls:ProductoApi", "La URL del servicio de productos no está configurada en appsettings.json");

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler);
        }

        public async Task<Transaccion> RegistrarTransaccionAsync(TransaccionDto dto, CancellationToken cancellationToken)
        {
            int cantidadAjuste = (dto.Tipo.ToLower() == "venta") ? -dto.Cantidad : dto.Cantidad;

            var url = $"{_productoServiceUrl}/api/productos/{dto.ProductoID}/ajustar-stock";
            var stockAjusteDto = new { CantidadAjuste = cantidadAjuste };

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, stockAjusteDto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al ajustar stock: {error}");
            }

            var transaccion = new Transaccion
            {
                Fecha = DateTime.UtcNow,
                Tipo = dto.Tipo,
                ProductoID = dto.ProductoID,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario,
                PrecioTotal = dto.Cantidad * dto.PrecioUnitario,
                Detalle = dto.Detalle
            };
            return await _repo.CrearAsync(transaccion);
        }

        public async Task<HistorialProductoDto> GetHistorialProductoAsync(int productoId, DateTime? fechaInicio, DateTime? fechaFin, string tipo, CancellationToken cancellationToken)
        {
            var transacciones = await _repo.GetByProductoIdAsync(productoId, fechaInicio, fechaFin, tipo);

            var url = $"{_productoServiceUrl}/api/productos/{productoId}";

            ProductoDetalleDto productoDetalle = null;

            try
            {
               productoDetalle = await _httpClient.GetFromJsonAsync<ProductoDetalleDto>(url);
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo obtener la información del producto {productoId}. Error: {ex.Message}");
            }

            if (productoDetalle == null)
            {
                throw new Exception($"Producto con ID {productoId} no encontrado en el servicio de productos.");
            }

            var historialRespuesta = new HistorialProductoDto
            {
                // Datos obtenidos del ProductoApi
                ProductoNombre = productoDetalle.Nombre,
                ProductoStockActual = productoDetalle.Stock,

                Historial = transacciones.Select(t => new TransaccionHistorialDto
                {
                    ID = t.ID,
                    Fecha = t.Fecha,
                    Tipo = t.Tipo,
                    Cantidad = t.Cantidad,
                    PrecioUnitario = t.PrecioUnitario,
                    PrecioTotal = t.PrecioTotal,
                    Detalle = t.Detalle
                }).ToList()
            };

            return historialRespuesta;
        }
    }
}
