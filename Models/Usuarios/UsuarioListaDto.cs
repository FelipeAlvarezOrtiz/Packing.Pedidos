using System.ComponentModel.DataAnnotations;

namespace Packing.Pedidos.Models.Usuarios
{
    public sealed class UsuarioListaDto
    {
        public required string Id { get; set; }
        [MaxLength(175)]
        public required string Nombres { get; set; }
        [MaxLength(125)]
        public required string Email { get; set; }
        public required string Rut { get; set; }
        public required string Dv { get; set; }
        public bool Activo { get; set; }
        [MaxLength(175)]
        public required string Cargo { get; set; }
        public int EmpresaId { get; set; }
        public required string Empresa { get; set; }
        public required string RoleId { get; set; }
        public string NumeroTelefonico { get; set; }
    }
}
