namespace LIBRERIA_M.Client.Models
{
    public class ClienteResumenDto
    {
        public int IdCliente { get; set; }
        public decimal TotalComprado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string EstadoFinanciero { get; set; } = string.Empty; // 'ConDeuda' o 'AlDia'

    }
}
