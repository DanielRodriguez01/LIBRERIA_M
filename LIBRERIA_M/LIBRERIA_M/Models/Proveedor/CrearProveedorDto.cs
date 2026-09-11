using System.ComponentModel.DataAnnotations;

namespace LIBRERIA_M.Models.Proveedor
{
    public class CrearProveedorDto
    {
        [Required(ErrorMessage = "El nombre o razón social es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El CUIT es obligatorio.")]
        [StringLength(20, ErrorMessage = "El CUIT no puede superar los 20 caracteres.")]
        public string CUIT { get; set; } = string.Empty;

        [StringLength(30, ErrorMessage = "El teléfono no puede superar los 30 caracteres.")]
        public string? Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [StringLength(150, ErrorMessage = "El email no puede superar los 150 caracteres.")]
        public string? Email { get; set; }

        [StringLength(200, ErrorMessage = "La dirección no puede superar los 200 caracteres.")]
        public string? Direccion { get; set; }
    }
}
