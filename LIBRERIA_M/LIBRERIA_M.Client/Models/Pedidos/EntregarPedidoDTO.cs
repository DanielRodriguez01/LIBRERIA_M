using System.ComponentModel.DataAnnotations;

namespace LIBRERIA_M.Client.Models.Pedidos
{
    public class EntregarPedidoDTO
    {
        [Range(0, 999999999.99, ErrorMessage = "El monto en efectivo no puede ser negativo.")]
        public decimal MontoEfectivo { get; set; }

        [Range(0, 999999999.99, ErrorMessage = "El monto por transferencia no puede ser negativo.")]
        public decimal MontoTransferencia { get; set; }
    }
}
