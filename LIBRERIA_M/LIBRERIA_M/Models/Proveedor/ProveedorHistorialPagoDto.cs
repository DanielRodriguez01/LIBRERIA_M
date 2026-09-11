namespace LIBRERIA_M.Models.Proveedor
{
    public class ProveedorHistorialPagoDto
    {
        public int IdPagoProveedor { get; set; }
        public int IdCompra { get; set; }
        public string? NumeroFactura { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
        public int? IdUsuario { get; set; }
    }
}
