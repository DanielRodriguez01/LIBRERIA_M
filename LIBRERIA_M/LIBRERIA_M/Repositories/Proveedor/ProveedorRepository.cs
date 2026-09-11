using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using LIBRERIA_M.Data;
using LIBRERIA_M.Models.Proveedor;

namespace LIBRERIA_M.Repositories.Proveedor
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public ProveedorRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<ProveedorDto>> ListarProveedores(bool soloActivos = true)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parametros = new DynamicParameters();
            parametros.Add("@SoloActivos", soloActivos, DbType.Boolean);

            return await connection.QueryAsync<ProveedorDto>(
                "dbo.sp_ListarProveedores",
                parametros,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<ProveedorDto>> BuscarProveedores(string? termino, bool soloActivos = true)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parametros = new DynamicParameters();
            parametros.Add("@Termino", termino, DbType.String);
            parametros.Add("@SoloActivos", soloActivos, DbType.Boolean);

            return await connection.QueryAsync<ProveedorDto>(
                "dbo.sp_BuscarProveedores",
                parametros,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<ProveedorDto?> ObtenerProveedorPorId(int idProveedor)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parametros = new DynamicParameters();
            parametros.Add("@IdProveedor", idProveedor, DbType.Int32);

            return await connection.QueryFirstOrDefaultAsync<ProveedorDto>(
                "dbo.sp_ObtenerProveedorPorId",
                parametros,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> CrearProveedor(CrearProveedorDto dto)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parametros = new DynamicParameters();
            parametros.Add("@Nombre", dto.Nombre, DbType.String);
            parametros.Add("@CUIT", dto.CUIT, DbType.String);
            parametros.Add("@Telefono", dto.Telefono, DbType.String);
            parametros.Add("@Email", dto.Email, DbType.String);
            parametros.Add("@Direccion", dto.Direccion, DbType.String);

            return await connection.ExecuteScalarAsync<int>(
                "dbo.sp_InsertarProveedor",
                parametros,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> ActualizarProveedor(ActualizarProveedorDto dto)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parametros = new DynamicParameters();
            parametros.Add("@IdProveedor", dto.IdProveedor, DbType.Int32);
            parametros.Add("@Nombre", dto.Nombre, DbType.String);
            parametros.Add("@CUIT", dto.CUIT, DbType.String);
            parametros.Add("@Telefono", dto.Telefono, DbType.String);
            parametros.Add("@Email", dto.Email, DbType.String);
            parametros.Add("@Direccion", dto.Direccion, DbType.String);

            var filasAfectadas = await connection.ExecuteAsync(
                "dbo.sp_ActualizarProveedor",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return filasAfectadas > 0 || filasAfectadas == -1;
        }

        public async Task<bool> DesactivarProveedor(int idProveedor)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parametros = new DynamicParameters();
            parametros.Add("@IdProveedor", idProveedor, DbType.Int32);

            var filasAfectadas = await connection.ExecuteAsync(
                "dbo.sp_DesactivarProveedor",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return filasAfectadas > 0 || filasAfectadas == -1;
        }

        public async Task<bool> ActivarProveedor(int idProveedor)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parametros = new DynamicParameters();
            parametros.Add("@IdProveedor", idProveedor, DbType.Int32);

            var filasAfectadas = await connection.ExecuteAsync(
                "dbo.sp_ActivarProveedor",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return filasAfectadas > 0 || filasAfectadas == -1;
        }

        public async Task<ProveedorResumenDto?> ObtenerResumenProveedor(int idProveedor)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parametros = new DynamicParameters();
            parametros.Add("@IdProveedor", idProveedor, DbType.Int32);

            return await connection.QueryFirstOrDefaultAsync<ProveedorResumenDto>(
                "dbo.sp_ResumenProveedor",
                parametros,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<ProveedorHistorialCompraDto>> ObtenerHistorialComprasProveedor(int idProveedor)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parametros = new DynamicParameters();
            parametros.Add("@IdProveedor", idProveedor, DbType.Int32);

            return await connection.QueryAsync<ProveedorHistorialCompraDto>(
                "dbo.sp_HistorialComprasProveedor",
                parametros,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<ProveedorHistorialPagoDto>> ObtenerHistorialPagosProveedor(int idProveedor)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parametros = new DynamicParameters();
            parametros.Add("@IdProveedor", idProveedor, DbType.Int32);

            return await connection.QueryAsync<ProveedorHistorialPagoDto>(
                "dbo.sp_HistorialPagosProveedor",
                parametros,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> RegistrarPagoProveedor(RegistrarPagoProveedorDto dto)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parametros = new DynamicParameters();
            parametros.Add("@IdProveedor", dto.IdProveedor, DbType.Int32);
            parametros.Add("@IdCompra", dto.IdCompra, DbType.Int32);
            parametros.Add("@Monto", dto.Monto, DbType.Decimal);
            parametros.Add("@TipoPago", string.IsNullOrWhiteSpace(dto.TipoPago) ? "Efectivo" : dto.TipoPago, DbType.String);
            parametros.Add("@Observaciones", dto.Observaciones, DbType.String);
            parametros.Add("@IdUsuario", dto.IdUsuario, DbType.Int32);
            parametros.Add("@FechaPago", dto.FechaPago, DbType.DateTime);

            var result = await connection.ExecuteAsync(
                "dbo.sp_RegistrarPagoProveedor",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return result > 0 || result == -1;
        }
    }
}