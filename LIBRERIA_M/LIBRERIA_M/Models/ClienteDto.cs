namespace LIBRERIA_M.Models
{
    public class ClienteDto
    {
        public int IdCliente { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string DNI { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public string? Direccion { get; set; }

        public string? Email { get; set; }

        public string TipoCliente { get; set; } = string.Empty;

        public decimal PorcentajeDescuento { get; set; }

        public bool Estado { get; set; }

        public decimal Deuda { get; set; } 

        public DateTime FechaAlta { get; set; } 
    }
}