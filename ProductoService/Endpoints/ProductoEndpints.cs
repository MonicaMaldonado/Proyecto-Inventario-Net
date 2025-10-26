using Microsoft.AspNetCore.Mvc;
using ProductoApi.Interface;

namespace ProductoApi.Endpoints;

public static class ProductoEndpints
{
    public static IEndpointRouteBuilder MapProductoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/productos")
            .WithTags("Productos");

        group.MapGet("/", async (
            [FromServices] IProductoService productoService,
            CancellationToken cancellationToken) =>
        {
            var result = await productoService.GetAllAsync(cancellationToken);

            return Results.Ok(result);
        })
            .WithName("GetProductos");



        group.MapGet("/{id:int}", async (
            [FromServices] IProductoService service,
            [FromRoute] int id,
            CancellationToken cancellationToken) =>
        {
            var producto = await service.GetByIdAsync(id, cancellationToken);
            if (producto == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(producto);
        })
        .WithName("GetProductoById");


        group.MapPost("/", async (
            [FromServices] IProductoService productoService,
            [FromBody] ProductoDto dto,
            CancellationToken cancellationToken) =>
        {
            var productoCreado = await productoService.CreateProductoAsync(dto, cancellationToken);

            return Results.Created($"/api/productos/{productoCreado.ID}", productoCreado);
        })
            .WithName("CreateProducto")
            .WithOpenApi()
            .WithSummary("Create a product");


        group.MapPut("/{id:int}", async (
            [FromServices] IProductoService productoService,
            [FromRoute] int id, 
            [FromBody] ProductoDto dto, 
            CancellationToken cancellationToken) =>
        {
            
            var exito = await productoService.UpdateProductoAsync(id, dto, cancellationToken);

            if (!exito)
            {
                return Results.NotFound(); 
            }


            return Results.NoContent();
        })
            .WithName("UpdateProducto")
            .WithOpenApi()
            .WithSummary("Update a product by ID");


        group.MapDelete("/{id:int}", async (
            [FromServices] IProductoService productoService,
            [FromRoute] int id, 
            CancellationToken cancellationToken) =>
        {
            var exito = await productoService.DeleteProductoAsync(id, cancellationToken);

            if (!exito)
            {
                return Results.NotFound(); 
            }

            return Results.NoContent();
        })
            .WithName("DeleteProducto")
            .WithOpenApi()
            .WithSummary("Delete a product by ID");


        group.MapPost("/{id:int}/ajustar-stock", async (
            [FromServices] IProductoService service,
            [FromRoute] int id,
            [FromBody] StockAjusteDto dto, 
            CancellationToken cancellationToken) =>
        {
            try
            {
                var exito = await service.AjustarStockAsync(id, dto.CantidadAjuste, cancellationToken);
                if (!exito)
                {
                    return Results.NotFound(new { mensaje = "Producto no encontrado." });
                }
                return Results.Ok(new { mensaje = "Stock actualizado correctamente." });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { mensaje = ex.Message });
            }
        })
        .WithName("AjustarStock")
        .WithSummary("Ajusta el stock de un producto (para uso interno).");


        return app;
    }

}

public class StockAjusteDto
{
    public int CantidadAjuste { get; set; }
}
