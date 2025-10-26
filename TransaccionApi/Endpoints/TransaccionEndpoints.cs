using Microsoft.AspNetCore.Mvc;
using TransaccionApi.Service;

namespace TransaccionApi.Endpoints
{
    public static class TransaccionEndpoints
    {
        public static IEndpointRouteBuilder MapTransaccionEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/transacciones")
                .WithTags("Transacciones");

            group.MapPost("/", async (
                [FromServices] ITransaccionService service,
                [FromBody] TransaccionDto dto,
                CancellationToken cancellationToken) =>
            {
                try
                {
                    var transaccion = await service.RegistrarTransaccionAsync(dto);
                    return Results.Created($"/api/transacciones/{transaccion.ID}", transaccion);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { mensaje = ex.Message });
                }
            });


            group.MapGet("/producto/{productoId:int}", async (
            [FromServices] ITransaccionService service,
            [FromRoute] int productoId,
            [FromQuery] DateTime? fechaInicio, 
            [FromQuery] DateTime? fechaFin,    
            [FromQuery] string? tipo,           
            CancellationToken cancellationToken) =>
            {
                try
                {
                    var historial = await service.GetHistorialProductoAsync(productoId, fechaInicio, fechaFin, tipo);
                    return Results.Ok(historial);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(new { mensaje = ex.Message });
                }
            })
        .WithName("GetHistorialPorProducto")
        .WithSummary("Obtiene el historial de un producto, con filtros opcionales.");


         return app;
        }
    }
}
