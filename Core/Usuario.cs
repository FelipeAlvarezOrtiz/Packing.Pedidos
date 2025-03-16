using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Packing.Pedidos.Core
{
    public sealed class Usuario : IdentityUser
    {
        [MaxLength(175)]
        public required string Nombres { get; set; }
        public int Rut { get; set; }
        [MaxLength(1)]
        public required string Dv { get; set; }
        public bool Activo { get; set; }
        [MaxLength(175)]
        public required string Cargo { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public bool PrimerLogin { get; set; }
    }
}
