namespace TransaccionApi;

    public record Transaccion
    {
        public int ID { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } // "compra" o "venta"
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
        public string Detalle { get; set; }
    }

