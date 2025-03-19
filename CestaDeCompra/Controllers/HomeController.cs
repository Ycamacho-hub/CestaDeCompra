using CestaDeCompra.Models;
using CestaDeCompra.Repository.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CestaDeCompra.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductoRepository _productoRepository;

        public HomeController(ILogger<HomeController> logger, IProductoRepository productoRepository)
        {
            _logger = logger;
            this._productoRepository = productoRepository;
        }

        public IActionResult Index()
        {
            return View(_productoRepository.GetProductos());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
