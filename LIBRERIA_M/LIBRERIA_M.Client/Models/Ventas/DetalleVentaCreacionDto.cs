namespace LIBRERIA_M.Client.Models.Ventas
{
    public class DetalleVentaCreacionDto
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
    }
}
