using System.ComponentModel.DataAnnotations;

namespace LIBRERIA_M.Models.Pedidos
{
    // Montos que el cliente abona al momento de entregar el pedido
    public class EntregarPedidoDTO
    {
        [Range(0, 999999999.99, ErrorMessage = "El monto en efectivo no puede ser negativo.")]
        public decimal MontoEfectivo { get; set; }

        [Range(0, 999999999.99, ErrorMessage = "El monto por transferencia no puede ser negativo.")]
        public decimal MontoTransferencia { get; set; }
    }
}