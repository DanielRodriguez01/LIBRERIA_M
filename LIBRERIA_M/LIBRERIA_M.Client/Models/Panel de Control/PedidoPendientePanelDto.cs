namespace LIBRERIA_M.Client.Models.Panel_de_Control
{
    public class PedidoPendientePanelDto
    {
        public int IdPedido { get; set; }
        public string NumeroPedido { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public DateTime FechaPedido { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string OrigenPedido { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
