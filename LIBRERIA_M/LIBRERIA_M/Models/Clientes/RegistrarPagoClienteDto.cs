namespace LIBRERIA_M.Models.Clientes
{
    public class RegistrarPagoClienteDto
    {
        public int IdCliente { get; set; }
        public decimal Monto { get; set; }
        public string TipoPago { get; set; } = "Efectivo";
        public string? Observaciones { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaPago { get; set; }
        public int? IdVenta { get; set; } // Null si es pago global FIFO, o ID de Venta específico
    }
}