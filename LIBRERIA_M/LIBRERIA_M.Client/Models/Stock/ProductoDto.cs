using System.ComponentModel.DataAnnotations;

namespace LIBRERIA_M.Client.Models.Stock
{
    public class ProductoDto
    {
        public int IdProducto { get; set; }

        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "La descripción no puede superar los 300 caracteres.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0, 999999999, ErrorMessage = "Ingrese un precio válido.")]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage = "El stock actual es obligatorio.")]
        [Range(0, 999999, ErrorMessage = "El Stock Actual debe estar entre 0 y 999999.")]
        public int StockActual { get; set; }

        [Required(ErrorMessage = "El stock mínimo es obligatorio.")]
        [Range(0, 999999, ErrorMessage = "El Stock Mínimo debe estar entre 0 y 999999.")]
        public int StockMinimo { get; set; }

        [Required]
        [Range(0, 999999999, ErrorMessage = "Ingrese un costo promedio válido.")]
        public decimal CostoPromedio { get; set; }

        [Required]
        public string Estado { get; set; } = "Activo";

        public string? Imagen { get; set; }

        // Propiedad calculada unificada: Stock bajo es cuando está por debajo o igual al mínimo
        public bool StockBajo => StockActual <= StockMinimo;

        // Precio de venta sugerido guardado desde la compra
        public decimal? PrecioSugerido { get; set; }
    }
}