using System.Collections.Generic;
using System.Threading.Tasks;
using LIBRERIA_M.Models.Proveedor;

namespace LIBRERIA_M.Repositories.Proveedor
{
    public interface IProveedorRepository
    {
        Task<IEnumerable<ProveedorDto>> ListarProveedores(bool soloActivos = true);
        Task<IEnumerable<ProveedorDto>> BuscarProveedores(string? termino, bool soloActivos = true);
        Task<ProveedorDto?> ObtenerProveedorPorId(int idProveedor);
        Task<int> CrearProveedor(CrearProveedorDto dto);
        Task<bool> ActualizarProveedor(ActualizarProveedorDto dto);
        Task<bool> DesactivarProveedor(int idProveedor);
        Task<bool> ActivarProveedor(int idProveedor);

        // Métodos financieros y de historial
        Task<ProveedorResumenDto?> ObtenerResumenProveedor(int idProveedor);
        Task<IEnumerable<ProveedorHistorialCompraDto>> ObtenerHistorialComprasProveedor(int idProveedor);
        Task<IEnumerable<ProveedorHistorialPagoDto>> ObtenerHistorialPagosProveedor(int idProveedor);
        Task<bool> RegistrarPagoProveedor(RegistrarPagoProveedorDto dto);
    }
}