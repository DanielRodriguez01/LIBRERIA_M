namespace LIBRERIA_M.Models.Clientes
{
    public class ClienteHistorialCompraDto
    {
        public int IdVenta { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public DateTime FechaVenta { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string Estado { get; set; } = string.Empty; // 'PAGADO', 'PENDIENTE', 'PARCIAL'
        public string? Observaciones { get; set; }
    }
}