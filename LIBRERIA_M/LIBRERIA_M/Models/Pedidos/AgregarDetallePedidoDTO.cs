using System;
using System.ComponentModel.DataAnnotations;

namespace LIBRERIA_M.Models.Pedidos
{
    public class AgregarDetallePedidoDTO
    {
        [Required(ErrorMessage = "El pedido es obligatorio.")]
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "El producto es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un producto válido.")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }
    }
}