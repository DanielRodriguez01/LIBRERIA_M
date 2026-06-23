using System.ComponentModel.DataAnnotations;

namespace LIBRERIA_M.Client.Models;

public class ClienteDto
{
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    public string Apellido { get; set; } = "";

    [Required(ErrorMessage = "El DNI es obligatorio.")]
    [RegularExpression(@"^\d{7,8}$",
        ErrorMessage = "El DNI debe contener entre 7 y 8 números.")]
    public string DNI { get; set; } = "";

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [StringLength(50)]
    public string Telefono { get; set; } = "";

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    [StringLength(200)]
    public string Direccion { get; set; } = "";

    [Required(ErrorMessage = "El email es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingrese un email válido.")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "El tipo de cliente es obligatorio.")]
    public string TipoCliente { get; set; } = "";

    public decimal PorcentajeDescuento { get; set; }

    public bool Estado { get; set; }
}