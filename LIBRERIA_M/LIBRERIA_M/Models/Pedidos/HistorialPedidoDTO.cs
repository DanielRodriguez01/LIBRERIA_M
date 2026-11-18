using System;

namespace LIBRERIA_M.Models.Pedidos
{
    // Una fila del historial de estados del pedido (tabla Pedido_Historial)
    public class HistorialPedidoDTO
    {
        public int IdHistorial { get; set; }
        public int IdPedido { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaCambio { get; set; }
        public string? Detalle { get; set; }
    }
}
