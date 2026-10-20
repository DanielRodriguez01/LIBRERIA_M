using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LIBRERIA_M.Client.Models.Pedidos
{
    public class CrearPedidoDTO
    {
        [Required(ErrorMessage = "El cliente es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un cliente válido.")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public int IdUsuario { get; set; }

        public DateTime? FechaEntrega { get; set; }

        [StringLength(300, ErrorMessage = "Las observaciones no pueden superar los 300 caracteres.")]
        public string? Observaciones { get; set; }

        [Required(ErrorMessage = "El origen del pedido es obligatorio.")]
        public string OrigenPedido { get; set; } = "Presencial";

        public List<AgregarDetallePedidoDTO> Detalles { get; set; } = new();
    }
}
