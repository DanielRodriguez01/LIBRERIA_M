namespace LIBRERIA_M.Client.Models.Ventas
{
    public class VentaListadoDto
    {
        public int IdVenta { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public DateTime FechaVenta { get; set; }
        public int IdCliente { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public string? ClienteDocumento { get; set; }
        public int IdUsuario { get; set; }
        public string UsuarioNombre { get; set; } = string.Empty;
        public decimal TotalVenta { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
    }
}
