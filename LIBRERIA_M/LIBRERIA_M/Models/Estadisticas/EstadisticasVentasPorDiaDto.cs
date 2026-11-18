namespace LIBRERIA_M.Models.Estadisticas
{
    public class EstadisticasVentasPorDiaDto
    {
        public DateTime Fecha { get; set; }

        public int CantidadVentas { get; set; }

        public decimal TotalVendido { get; set; }
    }
}
