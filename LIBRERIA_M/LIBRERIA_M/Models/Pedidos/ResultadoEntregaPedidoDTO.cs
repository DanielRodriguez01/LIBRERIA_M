namespace LIBRERIA_M.Models.Pedidos
{
    public class ResultadoEntregaPedidoDTO
    {
        public int IdPedido { get; set; }
        public string NumeroPedido { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public int? IdVenta { get; set; }
        public string? NumeroFactura { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string? EstadoVenta { get; set; }
    }
}
