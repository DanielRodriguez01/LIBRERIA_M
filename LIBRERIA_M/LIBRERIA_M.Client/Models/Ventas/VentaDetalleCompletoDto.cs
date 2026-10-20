namespace LIBRERIA_M.Client.Models.Ventas
{
    public class VentaDetalleCompletoDto
    {
        public int IdVenta { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public DateTime FechaVenta { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
        public int? IdPedido { get; set; }

        // Datos del Cliente
        public int IdCliente { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public string? ClienteDocumento { get; set; }
        public string? ClienteTelefono { get; set; }
        public string? ClienteEmail { get; set; }
        public string? ClienteDireccion { get; set; }
        public string? TipoCliente { get; set; }

        // Datos del Usuario / Vendedor
        public int IdUsuario { get; set; }
        public string UsuarioNombre { get; set; } = string.Empty;

        // Lista de renglones/productos
        public List<DetalleVentaItemDto> Detalles { get; set; } = new();
    }
}
