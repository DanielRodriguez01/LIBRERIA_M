using System.ComponentModel.DataAnnotations;

namespace LIBRERIA_M.Client.Models
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
        [Range(0.01, 9999999,
            ErrorMessage = "Ingrese un precio válido mayor a $0.")]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage = "El stock actual es obligatorio.")]
        [Range(0, 999999,
            ErrorMessage = "El Stock Actual debe estar entre 0 y 999999.")]
        public int StockActual { get; set; }

        [Required(ErrorMessage = "El stock mínimo es obligatorio.")]
        [Range(0, 999999,
            ErrorMessage = "El Stock Mínimo debe estar entre 0 y 999999.")]
        public int StockMinimo { get; set; }

        [Required]
        [Range(0, 9999999)]
        public decimal CostoPromedio { get; set; }

        [Required]
        public string Estado { get; set; } = "Activo";

        public string? Imagen { get; set; }

        public bool StockBajo =>
            StockActual > 0 && StockActual <= StockMinimo;
    }
}