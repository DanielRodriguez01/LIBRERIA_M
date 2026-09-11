namespace LIBRERIA_M.Models.Compra
{
    public class DetalleCompraDto
    {
        public int IdDetalleCompra { get; set; }
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}