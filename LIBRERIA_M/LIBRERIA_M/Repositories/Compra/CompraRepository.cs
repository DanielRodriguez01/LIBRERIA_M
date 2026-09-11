using System.Data;
using Dapper;
using LIBRERIA_M.Data;
using LIBRERIA_M.Models.Compra;
using LIBRERIA_M.Models.Proveedor;

namespace LIBRERIA_M.Repositories.Compra
{
    public class CompraRepository : ICompraRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public CompraRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<CompraListadoDto>> ListarCompras(string? busqueda, int? idProveedor, DateTime? fechaDesde, DateTime? fechaHasta, string? estado)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameters = new DynamicParameters();

            parameters.Add("@BuscarTexto", busqueda);
            parameters.Add("@IdProveedor", idProveedor);
            parameters.Add("@FechaDesde", fechaDesde);
            parameters.Add("@FechaHasta", fechaHasta);
            parameters.Add("@Estado", estado);

            return await connection.QueryAsync<CompraListadoDto>(
                "dbo.sp_ListarCompras",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<CompraDetalleCompletoDto?> ObtenerCompraPorId(int idCompra)
        {
            using var connection = _connectionFactory.CreateConnection();

            var compra = await connection.QueryFirstOrDefaultAsync<CompraDetalleCompletoDto>(
                "dbo.sp_ObtenerCompraPorId",
                new { IdCompra = idCompra },
                commandType: CommandType.StoredProcedure
            );

            if (compra == null) return null;

            var productos = await connection.QueryAsync<DetalleCompraDto>(
                "dbo.sp_ListarDetalleCompra",
                new { IdCompra = idCompra },
                commandType: CommandType.StoredProcedure
            );

            compra.Productos = productos.ToList();
            return compra;
        }

        public async Task<int> CrearCompra(CrearCompraDto compraDto)
        {
            using var connection = _connectionFactory.CreateConnection();

            // 1. Estructura DataTable según el TVP_DetalleCompra de SQL (4 columnas: IdProducto, Cantidad, CostoUnitario, PrecioSugerido)
            var tablaDetalles = new DataTable();
            tablaDetalles.Columns.Add("IdProducto", typeof(int));
            tablaDetalles.Columns.Add("Cantidad", typeof(int));
            tablaDetalles.Columns.Add("CostoUnitario", typeof(decimal));
            tablaDetalles.Columns.Add("PrecioSugerido", typeof(decimal));

            foreach (var item in compraDto.Detalles)
            {
                tablaDetalles.Rows.Add(
                    item.IdProducto,
                    item.Cantidad,
                    item.CostoUnitario,
                    (object?)item.PrecioVentaSugerido ?? DBNull.Value
                );
            }

            // 2. Parámetros exactos que recibe dbo.sp_InsertarCompra
            var parameters = new DynamicParameters();
            parameters.Add("@IdProveedor", compraDto.IdProveedor);
            parameters.Add("@IdUsuario", compraDto.IdUsuario);
            parameters.Add("@NumeroRemito", compraDto.NumeroRemito);
            parameters.Add("@Observaciones", compraDto.Observaciones);
            parameters.Add("@Detalle", tablaDetalles.AsTableValuedParameter("TVP_DetalleCompra"));

            // Parámetros para el primer pago registrado al crear la compra
            parameters.Add("@MontoInicialPagado", compraDto.MontoInicialPagado);
            parameters.Add("@TipoPagoInicial", compraDto.TipoPagoInicial ?? "EFECTIVO");

            // 3. Invocar sp_InsertarCompra
            var resultado = await connection.QueryFirstOrDefaultAsync<CompraResultadoDto>(
                "dbo.sp_InsertarCompra",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return resultado?.IdCompra ?? 0;
        }

        public async Task<bool> RegistrarPago(RegistrarPagoProveedorDto pagoDto)
        {
            using var connection = _connectionFactory.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@IdCompra", pagoDto.IdCompra);
            parameters.Add("@Monto", pagoDto.Monto);
            parameters.Add("@TipoPago", pagoDto.TipoPago);
            parameters.Add("@Observaciones", pagoDto.Observaciones);
            parameters.Add("@IdUsuario", pagoDto.IdUsuario);

            var resultado = await connection.QueryFirstOrDefaultAsync<CompraResultadoDto>(
                "dbo.sp_RegistrarPagoCompra",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return resultado != null;
        }

        public async Task<IEnumerable<ProveedorSelectorDto>> ObtenerProveedoresSelector()
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = "SELECT IdProveedor, Nombre FROM dbo.Proveedor WHERE Estado = 1 ORDER BY Nombre ASC";
            return await connection.QueryAsync<ProveedorSelectorDto>(query);
        }
    }
}