using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packing.Pedidos.Core.Productos;
using Packing.Pedidos.Data;
using Packing.Pedidos.Models.Productos;

namespace Packing.Pedidos.Controllers
{
    [Authorize]
    public class ConfiguracionesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ConfiguracionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ProductosIndex(CancellationToken cancellationToken)
        {
            var productos = await _context.Productos.AsNoTracking()
                .Include(x => x.Formato)
                .Include(x => x.Grupo)
                .Include(x => x.Presentacion)
                .Select(x => new ProductoListaDto()
                {
                    Formato = x.Formato.Nombre,
                    Grupo = x.Grupo.Nombre,
                    Id = x.Id,
                    Presentacion = x.Presentacion.Nombre,
                    Nombre = x.Nombre
                }).ToListAsync(cancellationToken);

            ViewBag.Formatos = _context.Formatos.ToList();
            ViewBag.Grupos = _context.Grupos.ToList();
            ViewBag.Presentaciones = _context.Presentaciones.ToList();

            return View(productos);
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> CrearProducto([FromForm] ProductoCreateDto request, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var formato = await _context.Formatos.FindAsync(request.FormatoId);
                var grupo = await _context.Grupos.FindAsync(request.GrupoId);
                var presentacion = await _context.Presentaciones.FindAsync(request.PresentacionId);
                if (formato is null || grupo is null || presentacion is null)
                {
                    return Json(new { success = false, errors = "La presentación, grupo o formato no son válidos." });
                }
                var productoNuevo = new Producto()
                {
                    Nombre = request.Nombre,
                    FormatoId = request.FormatoId,
                    NombreBusqueda = request.Nombre + " " + formato.Nombre + " " + grupo.Nombre + " " + presentacion.Nombre,
                    GrupoId = request.GrupoId,
                    PresentacionId = request.PresentacionId
                };
                await _context.Productos.AddAsync(productoNuevo, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        [HttpGet("[controller]/Producto/Obtener/{idProducto:int}")]
        public async Task<ActionResult> ObtenerById(int idProducto, CancellationToken cancellationToken)
        {
            var producto = await _context.Productos.Where(x => x.Id == idProducto)
                .Include(x => x.Formato)
                .Include(x => x.Grupo)
                .Include(x => x.Presentacion)
                .FirstOrDefaultAsync(cancellationToken);
            if (producto is null)
            {
                return Json(new { success = false, errors = "El producto no existe." });
            }

            return Json(producto);
        }


        [HttpDelete("[controller]/Producto/Eliminar/{idProducto:int}")]
        public IActionResult Eliminar(int idProducto)
        {
            var producto = _context.Productos.Find(idProducto);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


        [HttpPut("[controller]/Producto/[action]")]
        public async Task<ActionResult> Actualizar([FromBody] ProductoEditDto productoEdit, CancellationToken cancellationToken)
        {
            var producto = await _context.Productos.FindAsync(productoEdit.Id);
            if (producto is null)
            {
                return Json(new { success = false, errors = "El producto no existe." });
            }
            var formato = await _context.Formatos.FindAsync(productoEdit.FormatoId);
            var grupo = await _context.Grupos.FindAsync(productoEdit.GrupoId);
            var presentacion = await _context.Presentaciones.FindAsync(productoEdit.PresentacionId);
            if (formato is null || grupo is null || presentacion is null)
            {
                return Json(new { success = false, errors = "La presentación, grupo o formato no son válidos." });
            }
            producto.Nombre = productoEdit.Nombre;
            producto.FormatoId = productoEdit.FormatoId;
            producto.NombreBusqueda = productoEdit.Nombre + " " + formato.Nombre + " " + grupo.Nombre + " " +
                                      presentacion.Nombre;
            producto.GrupoId = productoEdit.GrupoId;
            producto.PresentacionId = productoEdit.PresentacionId;
            _context.Productos.Update(producto);

            await _context.SaveChangesAsync(cancellationToken);
            return Json(new { success = true });
        }
    }
}
