namespace TransaccionApi
{
    public record TransaccionDto
    {
        public string Tipo { get; set; } // "compra" o "venta"
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string Detalle { get; set; }
    }
}
