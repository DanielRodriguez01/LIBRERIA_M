namespace LIBRERIA_M.Client.Models.Compra
{
    public class CrearDetalleCompraDto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public int Cantidad { get; set; } = 1;
        public decimal CostoUnitario { get; set; }
        public decimal PrecioVentaSugerido { get; set; }
        public decimal Subtotal => Cantidad * CostoUnitario;
    }
}