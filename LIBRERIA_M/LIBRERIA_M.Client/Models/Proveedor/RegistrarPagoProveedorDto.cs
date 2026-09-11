namespace LIBRERIA_M.Client.Models.Proveedor
{
    public class RegistrarPagoProveedorDto
    {
        public int IdProveedor { get; set; }
        public int IdCompra { get; set; }
        public DateTime FechaPago { get; set; } = DateTime.Now;
        public decimal Monto { get; set; }
        public string TipoPago { get; set; } = "Efectivo";
        public string? Observaciones { get; set; }
        public int IdUsuario { get; set; }
    }
}