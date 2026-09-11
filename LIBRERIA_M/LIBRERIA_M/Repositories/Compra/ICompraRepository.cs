using LIBRERIA_M.Models.Compra;
using LIBRERIA_M.Models.Proveedor;

namespace LIBRERIA_M.Repositories.Compra
{
    public interface ICompraRepository
    {
        Task<IEnumerable<CompraListadoDto>> ListarCompras(string? busqueda, int? idProveedor, DateTime? fechaDesde, DateTime? fechaHasta, string? estado);
        Task<CompraDetalleCompletoDto?> ObtenerCompraPorId(int idCompra);
        Task<int> CrearCompra(CrearCompraDto compraDto);
        Task<bool> RegistrarPago(RegistrarPagoProveedorDto pagoDto);
        Task<IEnumerable<ProveedorSelectorDto>> ObtenerProveedoresSelector();
    }
}