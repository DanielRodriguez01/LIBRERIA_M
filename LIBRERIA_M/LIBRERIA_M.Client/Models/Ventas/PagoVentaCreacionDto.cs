using System.ComponentModel.DataAnnotations;

namespace LIBRERIA_M.Client.Models.Ventas
{
    public class PagoVentaCreacionDto
    {
        [Required(ErrorMessage = "El ID de la venta es obligatorio.")]
        public int IdVenta { get; set; }

        public int? IdCliente { get; set; }

        public int? IdUsuario { get; set; }

        [Required(ErrorMessage = "El monto del pago es obligatorio.")]
        [Range(0.01, 999999999.99, ErrorMessage = "El monto debe ser mayor a 0.")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "Debe especificar el tipo de pago.")]
        [StringLength(30, ErrorMessage = "El tipo de pago no puede superar los 30 caracteres.")]
        public string TipoPago { get; set; } = string.Empty; // Ej: 'Efectivo', 'Transferencia', 'Tarjeta'

        [StringLength(300, ErrorMessage = "Las observaciones no pueden superar los 300 caracteres.")]
        public string? Observaciones { get; set; }
    }
}

