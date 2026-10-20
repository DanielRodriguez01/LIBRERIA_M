using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LIBRERIA_M.Models.Panel_de_Control;

namespace LIBRERIA_M.Repositories.Panel_de_Control
{
    public interface IPanelControlRepository
    {
        Task<PanelControlResumenDto> ObtenerResumen(DateTime? fechaReferencia);
        Task<IEnumerable<VentaPorDiaDto>> ObtenerVentasPorDia(DateTime? fechaDesde, DateTime? fechaHasta);
        Task<IEnumerable<ProductoMasVendidoDto>> ObtenerProductosMasVendidos(int top);
        Task<IEnumerable<PedidoPendientePanelDto>> ObtenerPedidosPendientes(int cantidad);
    }
}
