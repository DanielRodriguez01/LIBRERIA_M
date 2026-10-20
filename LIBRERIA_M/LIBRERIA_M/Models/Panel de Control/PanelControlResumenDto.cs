namespace LIBRERIA_M.Models.Panel_de_Control
{
    public class PanelControlResumenDto
    {
        public decimal VentasMes { get; set; }
        public int CantidadVentas { get; set; }
        public decimal DeudasPendientes { get; set; }
        public int ClientesActivos { get; set; }
    }
}
