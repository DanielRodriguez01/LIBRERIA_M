namespace LIBRERIA_M.Client.Models.Pedidos
{
    public class PedidoDTO
    {
        public int IdPedido { get; set; }
        public string NumeroPedido { get; set; } = string.Empty;
        public DateTime FechaPedido { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? OrigenPedido { get; set; }
        public string? Observaciones { get; set; }
        public decimal Total { get; set; }
        public int? IdVenta { get; set; }
        public int? IdUsuario { get; set; }

        // Propiedades del Cliente
        public int IdCliente { get; set; }
        public string? NombreCliente { get; set; }
        public string? ApellidoCliente { get; set; }
        public string? DniCliente { get; set; }
        public string? TelefonoCliente { get; set; }
        public string? DireccionCliente { get; set; }
        public string? EmailCliente { get; set; }
        public string? TipoCliente { get; set; }
        public decimal PorcentajeDescuento { get; set; }

        // Propiedad calculada para mostrar el Nombre Completo sin fallas
        public string Cliente => !string.IsNullOrWhiteSpace(NombreCliente) || !string.IsNullOrWhiteSpace(ApellidoCliente)
            ? $"{NombreCliente} {ApellidoCliente}".Trim()
            : "Consumidor Final";

        // Detalle de productos asociados
        public List<DetallePedidoDTO> Detalle { get; set; } = new List<DetallePedidoDTO>();
    }
}
