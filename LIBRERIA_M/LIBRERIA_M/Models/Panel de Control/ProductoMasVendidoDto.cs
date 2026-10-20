namespace LIBRERIA_M.Models.Panel_de_Control
{
    public class ProductoMasVendidoDto
    {
        public int IdProducto { get; set; }
        public string Producto { get; set; } = string.Empty;
        public int CantidadVendida { get; set; }
    }
}
