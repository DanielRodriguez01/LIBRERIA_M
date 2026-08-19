namespace LIBRERIA_M.Client.Models
{
    public class ClienteHistorialPagoDto
    {
        public int IdPago { get; set; }
        public DateTime FechaPago { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string TipoPago { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
    }
}
