using System.Collections.Generic;
using System.Threading.Tasks;
using LIBRERIA_M.Models.Ventas;

namespace LIBRERIA_M.Repositories.Ventas
{
    public interface IVentaRepository
    {
        Task<int> InsertarVenta(VentaCreacionDto venta);
        Task InsertarDetalleVenta(int idVenta, DetalleVentaCreacionDto detalle);
        Task RecalcularTotalesVenta(int idVenta);
        Task<RespuestaPagoVentaDto?> RegistrarPagoVenta(PagoVentaCreacionDto pago);
        Task<IEnumerable<PagoVentaListadoDto>> ListarPagosVenta(int idVenta);
        Task<IEnumerable<VentaListadoDto>> ListarVentas(string? buscar, DateTime? fechaInicio, DateTime? fechaFin, string? estado);
        Task<VentaDetalleCompletoDto?> ObtenerVentaPorId(int idVenta);
        Task<IEnumerable<DetalleVentaItemDto>> ListarDetalleVenta(int idVenta);
        Task<NumeroFacturaDto> GenerarNumeroFacturaVenta();
    }
}
