using Packing.Pedidos.Core;
using System.ComponentModel.DataAnnotations;

namespace Packing.Pedidos.Models.Usuarios
{
    public class UsuarioCreatoDto
    {
        [MaxLength(175)]
        public required string Nombres { get; set; }
        [MaxLength(125)]
        public required string Email { get; set; }
        public string Rut { get; set; }
        public bool Activo { get; set; }
        [MaxLength(175)]
        public required string Cargo { get; set; }
        public int EmpresaId { get; set; }
        public Guid RoleId { get; set; }
        public string NumeroTelefonico { get; set; }
    }
}
