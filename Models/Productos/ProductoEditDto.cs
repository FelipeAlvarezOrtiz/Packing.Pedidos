using System.ComponentModel.DataAnnotations;

namespace Packing.Pedidos.Models.Productos
{
    public sealed class ProductoEditDto
    {
        public int Id { get; set; }
        [MaxLength(225)]
        public required string Nombre { get; set; }
        public int PresentacionId { get; set; }
        public int FormatoId { get; set; }
        public int GrupoId { get; set; }
    }
}
