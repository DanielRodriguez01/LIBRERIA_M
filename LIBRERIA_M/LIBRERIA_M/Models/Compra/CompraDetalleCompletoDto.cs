namespace LIBRERIA_M.Models.Compra
{
    public class CompraDetalleCompletoDto
    {
        public int IdCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public string? NumeroFactura { get; set; }
        public string? NumeroRemito { get; set; }
        public decimal TotalCompra { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string Estado { get; set; } = "PAGADO";
        public string? Observaciones { get; set; }
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; } = string.Empty;

        public List<DetalleCompraDto> Productos { get; set; } = new();
    }
}