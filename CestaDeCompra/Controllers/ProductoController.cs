using Microsoft.AspNetCore.Mvc;
using CestaDeCompra.Models;
using CestaDeCompra.Data;
using System.Data;


namespace CestaDeCompra.Controllers
{
    public class ProductoController : Controller
    {

        public ProductoRepo prodRepo = new();

        public IActionResult Index()
        {
            return View(prodRepo.GetProductos());
        }

        [HttpGet]
        public IActionResult Guardar(string codigo)
        {

            const string SessionKeyBuy = "_compra";
            const string SessionKeyList = "_listaPrductos";

            Producto p = new();
            int numCompra = 0;

            if(HttpContext.Session.GetInt32(SessionKeyBuy) == null)
            {
                numCompra = 0;
                HttpContext.Session.SetInt32(SessionKeyBuy, numCompra+1);
                //ViewData["compra"] = numCompra + 1;
            }
            else
            {
                numCompra = (int) HttpContext.Session.GetInt32(SessionKeyBuy);
                HttpContext.Session.SetInt32(SessionKeyBuy, numCompra + 1);
            }

            if (HttpContext.Session.GetString(SessionKeyList) == null) 
            {
                    
            }

            ViewData["cod"] = codigo;


             
            return LocalRedirect ("/producto/index");
        }

        public string jsonProducto(Producto p)
        {
            return "{'cod': }";
        }


    }
}
