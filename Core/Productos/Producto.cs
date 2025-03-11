using System.ComponentModel.DataAnnotations;

namespace Packing.Pedidos.Core.Productos
{
    public sealed class Producto
    {
        public int Id { get; set; }
        [MaxLength(225)]
        public required string Nombre { get; set; }
        [MaxLength(750)]
        public required string NombreBusqueda { get; set; }
        public int FormatoId { get; set; }
        public Formato Formato { get; set; }
        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }
        public int PresentacionId { get; set; }
        public Presentacion Presentacion { get; set; }
    }
}
