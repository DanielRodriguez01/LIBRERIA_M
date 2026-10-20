using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LIBRERIA_M.Repositories.Pedido
{
    public interface IPedidoRepository
    {
        // Consultas / Lectura utilizando rutas explícitas
        Task<IEnumerable<LIBRERIA_M.Models.Pedidos.PedidoListadoDto>> ListarPedidos(string? estado = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null, string? buscar = null);
        Task<IEnumerable<LIBRERIA_M.Models.Pedidos.PedidoDTO>> ListarPedidosPendientesPanel(int cantidad = 10);
        Task<LIBRERIA_M.Models.Pedidos.PedidoDTO?> ObtenerPorId(int idPedido);
        Task<IEnumerable<LIBRERIA_M.Models.Pedidos.DetallePedidoDTO>> ListarDetallePedido(int idPedido);

        // Operaciones de Escritura
        Task<int> InsertarPedido(LIBRERIA_M.Models.Pedidos.CrearPedidoDTO dto);
        Task InsertarDetallePedido(LIBRERIA_M.Models.Pedidos.AgregarDetallePedidoDTO dto);
        Task ActualizarPedido(LIBRERIA_M.Models.Pedidos.ActualizarPedidoDTO dto);
        Task EliminarDetallePedido(int idDetallePedido);

        // Flujo de Estados
        Task TerminarPedido(int idPedido);
        Task EntregarPedido(int idPedido);
        Task CancelarPedido(int idPedido);
    }
}