using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Packing.Pedidos.Core;
using Packing.Pedidos.Data;
using Packing.Pedidos.Interfaces;
using Packing.Pedidos.Models.Usuarios;

namespace Packing.Pedidos.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMailer _mailer;

        public UsuariosController(ILogger<UsuariosController> logger, ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<Usuario> userManager, IWebHostEnvironment env, IMailer mailer)
        {
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _env = env;
            _mailer = mailer;
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
            ViewBag.Roles = new SelectList(_context.Roles, "Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioCreatoDto userCreateDto)
        {
            if (ModelState.IsValid)
            {
                var splitRut = userCreateDto.Rut.Replace(".", "").Split("-");
                var usuario = new Usuario()
                {
                    Cargo = userCreateDto.Cargo,
                    Dv = splitRut[1],
                    Nombres = userCreateDto.Nombres,
                    Activo = userCreateDto.Activo,
                    Email = userCreateDto.Email,
                    EmpresaId = userCreateDto.EmpresaId,
                    PhoneNumber = userCreateDto.NumeroTelefonico,
                    Rut = Convert.ToInt32(splitRut[0]),
                    UserName = userCreateDto.Email,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PrimerLogin = false
                };
                var role = await _roleManager.FindByNameAsync(userCreateDto.RoleId);
                if (role is null)
                {
                    ModelState.AddModelError("Resultado","El role no existe");
                    return View("Registrar", userCreateDto);
                }
                var resultCreate = await _userManager.CreateAsync(usuario, "Colonos.2025-");
                if (!resultCreate.Succeeded)
                {
                    _logger.LogError(resultCreate.Errors.ToList().ToString());
                    return View("Registrar", userCreateDto);
                }

                await _userManager.AddToRoleAsync(usuario, userCreateDto.RoleId);
                await _context.SaveChangesAsync();

                var filePath = Path.Combine(_env.WebRootPath, "templates", "template_correo_jp.html");

                if (!System.IO.File.Exists(filePath))
                    _logger.LogWarning("EL ARCHIVO TEMPLATE PARA PRIMER LOGIN NO EXISTE");
                else
                {
                    var content = await System.IO.File.ReadAllTextAsync(filePath);
                    content = content.Replace("{{PASSWORD}}", "Colonos.2025-");
                    
                    await _mailer.EnviarEmail([usuario.Email], 
                        ["mbertolla@loscolonos.cl", "me@felipealvarez.dev"], 
                        "Creación de usuario en sistema", 
                        content);
                }
                return RedirectToAction(nameof(Registrar));
            }

            ViewBag.Empresas = new SelectList(_context.Empresas, "Id", "Nombre", userCreateDto.EmpresaId);
            ViewBag.Roles = new SelectList(_context.Roles, "Id", "Name");

            return View("Registrar", userCreateDto);
        }

    }
}
