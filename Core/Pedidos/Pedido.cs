using System.ComponentModel.DataAnnotations;

namespace Packing.Pedidos.Core.Pedidos
{
    public sealed class Pedido
    {
        public Guid Id { get; set; }
        [MaxLength(125)]
        [EmailAddress]
        public required string Email { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime FechaActualizacion { get; set; }
        [MaxLength(950)]
        public string? Observacion { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public List<DetallePedido> Detalles { get; set; }
    }
}
