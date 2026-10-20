namespace LIBRERIA_M.Client.Models.Clientes
{
    public class ClienteHistorialPagoDto
    {
        public int IdPago { get; set; }
        public int IdVenta { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; } = string.Empty; // Mapea con TipoPago AS MetodoPago
        public string? Observaciones { get; set; }
        public int? IdUsuario { get; set; }
    }
}
