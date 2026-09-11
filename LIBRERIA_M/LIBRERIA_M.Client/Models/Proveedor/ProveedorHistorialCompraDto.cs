namespace LIBRERIA_M.Client.Models.Proveedor
{
    public class ProveedorHistorialCompraDto
    {
        public int IdCompra { get; set; }
        public string? NumeroFactura { get; set; }
        public string? NumeroRemito { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal TotalCompra { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
    }
}
