namespace LIBRERIA_M.Models.Estadisticas
{
    public class EstadisticasProductoMasVendidoDto
    {
        public long Ranking { get; set; }

        public int IdProducto { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public int CantidadVendida { get; set; }

        public decimal TotalFacturado { get; set; }
    }
}
