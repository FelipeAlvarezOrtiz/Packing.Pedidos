using System.ComponentModel.DataAnnotations;

namespace Packing.Pedidos.Core.Productos
{
    public sealed class Presentacion
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public required string Nombre { get; set; }
    }
}
