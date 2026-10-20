using System;
using System.ComponentModel.DataAnnotations;

namespace LIBRERIA_M.Models.Pedidos
{
    public class ActualizarPedidoDTO
    {
        [Required]
        public int IdPedido { get; set; }

        public DateTime? FechaEntrega { get; set; }

        [StringLength(300, ErrorMessage = "Las observaciones no pueden superar los 300 caracteres.")]
        public string? Observaciones { get; set; }

        public string? OrigenPedido { get; set; }
    }
}