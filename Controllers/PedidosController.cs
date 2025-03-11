using Microsoft.AspNetCore.Mvc;

namespace Packing.Pedidos.Controllers
{
    public class PedidosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
