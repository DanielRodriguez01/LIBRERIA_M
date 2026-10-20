namespace LIBRERIA_M.Client.Models.Ventas
{
    public class RespuestaPagoVentaDto
    {
        public int IdVenta { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public decimal TotalVenta { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
