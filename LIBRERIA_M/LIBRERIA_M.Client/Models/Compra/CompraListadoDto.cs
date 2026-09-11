namespace LIBRERIA_M.Client.Models.Compra
{
    public class CompraListadoDto
    {
        public int IdCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public string? NumeroFactura { get; set; }
        public string? NumeroRemito { get; set; }
        public decimal TotalCompra { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string Estado { get; set; } = "PAGADO";
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; } = string.Empty;
    }
}