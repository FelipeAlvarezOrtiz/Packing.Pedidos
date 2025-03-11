using System.ComponentModel.DataAnnotations;

namespace Packing.Pedidos.Core
{
    public sealed class Empresa
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public required string Nombre { get; set; }
        public int Rut { get; set; }
        [MaxLength(1)]
        public required string Dv { get; set; }
        public bool Activo { get; set; }
        public List<Contacto> Contactos { get; set; } = new();
    }
}
