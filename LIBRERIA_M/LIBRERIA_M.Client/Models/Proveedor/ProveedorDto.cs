namespace LIBRERIA_M.Client.Models.Proveedor
{
    public class ProveedorDto
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string CUIT { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; } // Campo agregado

    }
}
