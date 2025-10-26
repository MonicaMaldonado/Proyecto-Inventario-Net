namespace TransaccionApi;

public record TransaccionHistorialDto
{
    public int ID { get; set; }
    public DateTime Fecha { get; set; }
    public string Tipo { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal PrecioTotal { get; set; }
    public string Detalle { get; set; }
}

public record HistorialProductoDto
{
    public string ProductoNombre { get; set; }
    public int ProductoStockActual { get; set; }
    public List<TransaccionHistorialDto> Historial { get; set; }
}

public record ProductoDetalleDto
{
    public string Nombre { get; set; }
    public int Stock { get; set; }
}
