namespace LIBRERIA_M.Client.Models.Compra 
{
    public class CompraResultadoDto
    {
        public int IdCompra { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public decimal TotalCompra { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}