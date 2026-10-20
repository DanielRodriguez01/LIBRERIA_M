namespace LIBRERIA_M.Client.Models.Panel_de_Control
{
    public class PanelControlDashboardDto
    {
        public PanelControlResumenDto Resumen { get; set; } = new PanelControlResumenDto();
        public List<VentaPorDiaDto> VentasPorDia { get; set; } = new List<VentaPorDiaDto>();
        public List<ProductoMasVendidoDto> ProductosMasVendidos { get; set; } = new List<ProductoMasVendidoDto>();
        public List<PedidoPendientePanelDto> PedidosPendientes { get; set; } = new List<PedidoPendientePanelDto>();
    }
}
