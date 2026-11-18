namespace LIBRERIA_M.Client.Models.Estadisticas
{
    public class EstadisticasResumenComprasDto
    {
        public decimal ComprasTotales { get; set; }

        public int CantidadCompras { get; set; }

        public decimal PromedioCompra { get; set; }

        public decimal TotalPagado { get; set; }

        public decimal SaldoPendiente { get; set; }

        public DateTime? UltimaCompra { get; set; }
    }
}
