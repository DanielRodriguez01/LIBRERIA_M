namespace LIBRERIA_M.Models.Proveedor
{
    public class ProveedorResumenDto
    {
        public int IdProveedor { get; set; }
        public decimal TotalComprado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string EstadoFinanciero { get; set; } = string.Empty; // "AL DIA" o "CON DEUDA"

    }
}
