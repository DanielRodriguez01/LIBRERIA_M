using System;
using System.ComponentModel.DataAnnotations;

namespace LIBRERIA_M.Client.Models.Pedidos
{
    public class ActualizarEntregaPedidoDTO
    {
        // "RETIRO" (retira en el local) o "ENVIO" (enviar pedido)
        [Required(ErrorMessage = "La modalidad de entrega es obligatoria.")]
        public string TipoEntrega { get; set; } = "RETIRO";

        // Solo se usan cuando TipoEntrega = "ENVIO"
        [StringLength(250, ErrorMessage = "La dirección de entrega no puede superar los 250 caracteres.")]
        public string? DireccionEntrega { get; set; }

        public TimeSpan? HorarioEntrega { get; set; }
    }
}
