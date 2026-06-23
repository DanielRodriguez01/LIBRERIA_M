using System.ComponentModel.DataAnnotations;

namespace LIBRERIA_M.Models
{
    public class ProductoDto
    {
        public int IdProducto { get; set; }

        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0, 999999999)]
        public decimal PrecioUnitario { get; set; }

        [Required]
        [Range(0, 999999)]
        public int StockActual { get; set; }

        [Required]
        [Range(0, 999999)]
        public int StockMinimo { get; set; }

        [Required]
        [Range(0, 999999999)]
        public decimal CostoPromedio { get; set; }

        [Required]
        public string Estado { get; set; } = "Activo";

        public string? Imagen { get; set; }

        // Propiedad calculada para el Front-End
        public bool StockBajo => StockActual <= StockMinimo;
    }
}
