using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LIBRERIA_M.Models.Pedidos
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

        // Modalidad de entrega: "RETIRO" (retira en el local) o "ENVIO" (enviar pedido)
        [Required(ErrorMessage = "La modalidad de entrega es obligatoria.")]
        public string TipoEntrega { get; set; } = "RETIRO";

        // Solo se usan cuando TipoEntrega = "ENVIO"
        [StringLength(250, ErrorMessage = "La dirección de entrega no puede superar los 250 caracteres.")]
        public string? DireccionEntrega { get; set; }

        public TimeSpan? HorarioEntrega { get; set; }

        public List<AgregarDetallePedidoDTO> Detalles { get; set; } = new();
    }
}
