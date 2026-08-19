using System.ComponentModel.DataAnnotations;

namespace LIBRERIA_M.Client.Models
{
    public class ClienteDto
    {
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [RegularExpression(@"^\d{7,8}$", ErrorMessage = "El DNI debe contener entre 7 y 8 números.")]
        public string DNI { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        public string? Telefono { get; set; }

        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        public string? Direccion { get; set; }

        [EmailAddress(ErrorMessage = "Ingrese un email válido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El tipo de cliente es obligatorio.")]
        public string TipoCliente { get; set; } = "Normal";

        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100.")]
        public decimal PorcentajeDescuento { get; set; }

        public bool Estado { get; set; } = true;

        
        public decimal Deuda { get; set; }

        public DateTime FechaAlta { get; set; } 
    }
}