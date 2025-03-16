using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Packing.Pedidos.Core;
using Packing.Pedidos.Core.Pedidos;
using Packing.Pedidos.Core.Productos;
using Packing.Pedidos.Data;
using Packing.Pedidos.Models.Usuarios;

namespace Packing.Pedidos.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Usuario> _userManager;

        public UsuariosController(ILogger<UsuariosController> logger, ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<Usuario> userManager)
        {
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult EmpresasIndex()
        {
            return View();
        }

        public async Task<IActionResult> ListaUsuarios()
        {
            var usuarios = await _context.Usuarios.AsNoTracking()
                .Include(x => x.Empresa)
                .Select(x => new UsuarioListaDto()
                {
                    Nombres = x.Nombres,
                    Empresa = x.Empresa.Nombre,
                    Dv = x.Dv,
                    Cargo = x.Cargo,
                    Email = x.Email ?? "-",
                    Id = x.Id,
                    Rut = x.Rut.ToString(),
                    NumeroTelefonico = x.PhoneNumber?? "-",
                    EmpresaId = x.EmpresaId,
                    Activo = x.Activo,
                    RoleId = string.Empty
                }).ToListAsync();
            foreach (var usuario in usuarios)
            {
                var roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(usuario.Id));
                usuario.RoleId = string.Join(", ", roles); // Concatenamos roles en una sola cadena
            }
            return View(usuarios);
        }

        public IActionResult Registrar()
        {
            ViewBag.Empresas = new SelectList(_context.Empresas, "Id", "Nombre");
            ViewBag.Roles = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.UserName = usuario.Email;

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Registrar));
            }

            ViewBag.Empresas = new SelectList(_context.Empresas, "Id", "Nombre", usuario.EmpresaId);
            ViewBag.Roles = new SelectList(_context.Roles, "Id", "Name");

            return View("Registrar",usuario);
        }

    }
}
