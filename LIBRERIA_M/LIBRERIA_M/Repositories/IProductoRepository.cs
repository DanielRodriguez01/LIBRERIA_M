using LIBRERIA_M.Models;

namespace LIBRERIA_M.Repositories
{
    public interface IProductoRepository
    {
        Task<IEnumerable<ProductoDto>> ListarProductos();

        Task<ProductoDto?> ObtenerProductoPorId(int idProducto);

        Task CrearProducto(ProductoDto producto);

        Task ActualizarProducto(ProductoDto producto);

        Task DesactivarProducto(int idProducto);

        Task ReactivarProducto(int idProducto);
    }
}