using Dapper;
using LIBRERIA_M.Data;
using LIBRERIA_M.Models;

namespace LIBRERIA_M.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public ProductoRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<ProductoDto>> ListarProductos()
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<ProductoDto>(
                "sp_ListarProductos",
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<ProductoDto?> ObtenerProductoPorId(int idProducto)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<ProductoDto>(
                "sp_ObtenerProductoPorId",
                new
                {
                    IdProducto = idProducto
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task CrearProducto(ProductoDto producto)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_InsertarProducto",
                new
                {
                    producto.Nombre,
                    producto.Descripcion,
                    producto.PrecioUnitario,
                    producto.StockActual,
                    producto.StockMinimo,
                    producto.CostoPromedio,
                    producto.Estado,
                    producto.Imagen
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task ActualizarProducto(ProductoDto producto)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_ActualizarProducto",
                new
                {
                    producto.IdProducto,
                    producto.Nombre,
                    producto.Descripcion,
                    producto.PrecioUnitario,
                    producto.StockActual,
                    producto.StockMinimo,
                    producto.CostoPromedio,
                    producto.Estado,
                    producto.Imagen
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task DesactivarProducto(int idProducto)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_DesactivarProducto",
                new
                {
                    IdProducto = idProducto
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task ReactivarProducto(int idProducto)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_ReactivarProducto",
                new
                {
                    IdProducto = idProducto
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ProductoDto>> BuscarProductos(string texto)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<ProductoDto>(
                "sp_ListarProductos",
                new
                {
                    Busqueda = texto,
                    FiltroStock = "Todos"
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}