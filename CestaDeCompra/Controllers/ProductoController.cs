using Microsoft.AspNetCore.Mvc;
using CestaDeCompra.Models;
using CestaDeCompra.Data;


namespace CestaDeCompra.Controllers
{
    public class ProductoController : Controller
    {

        public ProductoRepo prodRepo = new();

        public IActionResult Index()
        {
            return View(prodRepo.GetProductos());
        }
    }
}
