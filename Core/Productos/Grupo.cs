using System.ComponentModel.DataAnnotations;

namespace Packing.Pedidos.Core.Productos
{
    public sealed class Grupo
    {
        public int Id { get; set; }
        [MaxLength(120)]
        public required string Nombre { get; set; }
        [MaxLength(950)]
        public required string Url { get; set; }
    }
}
