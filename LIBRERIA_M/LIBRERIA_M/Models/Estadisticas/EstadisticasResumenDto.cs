namespace LIBRERIA_M.Models.Estadisticas
{
    public class EstadisticasResumenDto
    {
        public decimal VentasTotales { get; set; }

        public decimal ComprasTotales { get; set; }

        public decimal GananciaEstimada { get; set; }

        public int CantidadVentas { get; set; }

        public int ProductosVendidos { get; set; }
    }
}
