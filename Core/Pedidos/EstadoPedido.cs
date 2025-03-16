using System.ComponentModel.DataAnnotations;

namespace Packing.Pedidos.Core.Pedidos
{
    public sealed class EstadoPedido
    {
        public int Id { get; set; }
        [MaxLength(75)]
        public required string Nombre { get; set; }
    }
}
