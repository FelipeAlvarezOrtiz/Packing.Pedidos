using System.ComponentModel.DataAnnotations;

namespace Packing.Pedidos.Core
{
    public sealed class Contacto
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public required string Nombre { get; set; }
        [MaxLength(15)]
        public string? Celular { get; set; }
        [MaxLength(75)]
        public string? Email { get; set; }
        [MaxLength(150)]
        public string? Referencia { get; set; }
        public Empresa Empresa { get; set; }
        public int EmpresaId { get; set; }
    }
}
