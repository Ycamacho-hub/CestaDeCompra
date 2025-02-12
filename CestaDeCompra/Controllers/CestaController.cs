using Microsoft.AspNetCore.Mvc;

namespace CestaDeCompra.Controllers
{
    public class CestaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
