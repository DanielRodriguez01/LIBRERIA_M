namespace LIBRERIA_M.Models
{
    public class ClienteHistorialCompraDto
    {
        public int IdVenta { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public DateTime FechaVenta { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public bool Estado { get; set; }

    }
}
