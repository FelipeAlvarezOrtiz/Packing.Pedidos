using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Packing.Pedidos.Core
{
    public sealed class Usuario : IdentityUser
    {
        [MaxLength(175)]
        public required string Nombres { get; set; }
    }
}
