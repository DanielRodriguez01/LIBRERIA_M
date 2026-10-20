namespace LIBRERIA_M.Models.Ventas
{
    public class VentaCreacionDto
    {
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public int? IdPedido { get; set; }
        public string? Observaciones { get; set; }
        public decimal MontoInicialPagado { get; set; } = 0;
        public string TipoPagoInicial { get; set; } = "EFECTIVO";
        public List<DetalleVentaCreacionDto> Detalles { get; set; } = new();
    }
}