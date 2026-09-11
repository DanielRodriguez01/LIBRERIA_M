namespace LIBRERIA_M.Models.Compra
{
    public class CrearDetalleCompraDto
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal PrecioVentaSugerido { get; set; } // CostoUnitario * 2
    }
}