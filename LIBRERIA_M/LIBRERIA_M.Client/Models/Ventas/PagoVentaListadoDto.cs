using System;

namespace LIBRERIA_M.Client.Models.Ventas
{
    public class PagoVentaListadoDto
    {
        public int IdPago { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string TipoPago { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}
