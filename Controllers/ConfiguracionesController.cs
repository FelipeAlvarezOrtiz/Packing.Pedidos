using Microsoft.AspNetCore.Mvc;

namespace Packing.Pedidos.Controllers
{
    public class ConfiguracionesController : Controller
    {
        public IActionResult ProductosIndex()
        {
            return View();
        }
    }
}
