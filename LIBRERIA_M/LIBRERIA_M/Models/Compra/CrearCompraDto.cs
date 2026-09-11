namespace LIBRERIA_M.Models.Compra
{
    public class CrearCompraDto
    {
        public DateTime FechaCompra { get; set; } = DateTime.Now;
        public string? NumeroFactura { get; set; }
        public string? NumeroRemito { get; set; }
        public string? Observaciones { get; set; }
        public int IdProveedor { get; set; }
        public int IdUsuario { get; set; }
        public decimal MontoInicialPagado { get; set; }
        public string? TipoPagoInicial { get; set; } = "EFECTIVO";

        public List<CrearDetalleCompraDto> Detalles { get; set; } = new();
    }
}