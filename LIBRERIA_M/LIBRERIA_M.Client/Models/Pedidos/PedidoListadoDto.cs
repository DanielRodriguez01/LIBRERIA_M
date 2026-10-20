namespace LIBRERIA_M.Client.Models.Pedidos
{
    public class PedidoListadoDto
    {
        public int IdPedido { get; set; }
        public string NumeroPedido { get; set; } = string.Empty;
        public DateTime FechaPedido { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string Estado { get; set; } = string.Empty;

        public string OrigenPedido { get; set; } = string.Empty;
        public string Origen => OrigenPedido;

        public decimal Total { get; set; }
        public string? Observaciones { get; set; }
        public int IdUsuario { get; set; }
        public int IdCliente { get; set; }

        public string Cliente { get; set; } = string.Empty;
        public string ClienteNombre => Cliente;

        public string? DniCliente { get; set; }
    }
}
