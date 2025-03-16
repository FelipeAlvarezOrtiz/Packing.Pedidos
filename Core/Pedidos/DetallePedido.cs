using System.ComponentModel.DataAnnotations;

namespace Packing.Pedidos.Core.Pedidos
{
    public sealed class DetallePedido
    {
        public int Id { get; set; }
        public int CantidadDelFormato { get; set; }
        public int CantidadTotalUnitaria { get; set; }
        [MaxLength(175)]
        public required string ProductoNombre { get; set; }
        [MaxLength(75)]
        public required string FormatoNombre { get; set; }
        public int FormatoCantidad { get; set; }
        [MaxLength(125)]
        public required string GrupoNombre { get; set; }
        [MaxLength(175)]
        public required string PresentacionNombre { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public bool Procesado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaElaboracion { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaVencimiento { get; set; }

        // REFERENCIAS
        public Guid PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public int EstadoPedidoId { get; set; }
        public EstadoPedido EstadoPedido { get; set; }
    }
}
