using System.ComponentModel.DataAnnotations;

namespace Packing.Pedidos.Core.Productos
{
    public sealed class Formato
    {
        public int Id { get; set; }
        [MaxLength(125)]
        public required string Nombre { get; set; }
        [Range(1,999999)]
        public int Cantidad { get; set; }
    }
}
