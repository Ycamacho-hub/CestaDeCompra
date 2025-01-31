using Microsoft.AspNetCore.Mvc;
using CestaDeCompra.Models;
using CestaDeCompra.Data;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace CestaDeCompra.Controllers
{
    public class ProductoController : Controller
    {

        public ProductoRepo prodRepo = new();
        const string SessionKeyBuy = "_compra";
        const string SessionKeyList = "_listaPrductos";

        public IActionResult Index()
        {
            return View(prodRepo.GetProductos());
        }

        [HttpGet]
        public IActionResult Guardar(string productCode)
        {
            Producto p = new();
            int numCompra = 0;
            List<Producto> listaProductos = [];

            if(HttpContext.Session.GetInt32(SessionKeyBuy) == null)
            {
                numCompra = 0;
                HttpContext.Session.SetInt32(SessionKeyBuy, numCompra+1);
                //ViewData["compra"] = numCompra + 1;

                listaProductos.Add(prodRepo.GetProducto(productCode));
                string json = JsonSerializer.Serialize(listaProductos);
                HttpContext.Session.SetString(SessionKeyList, json);
            }
            else
            {
                numCompra = (int) HttpContext.Session.GetInt32(SessionKeyBuy);
                HttpContext.Session.SetInt32(SessionKeyBuy, numCompra + 1);

                string jsonP = HttpContext.Session.GetString(SessionKeyList);
                listaProductos = JsonSerializer.Deserialize<List<Producto>>(jsonP);
                listaProductos.Add(prodRepo.GetProducto(productCode));
                jsonP = JsonSerializer.Serialize(listaProductos);
                HttpContext.Session.SetString(SessionKeyList, jsonP);
            }

            return LocalRedirect ("/producto/index");
        }


        public IActionResult Cesta()
        {
            var listaProductos = new List<Producto>();

            if(HttpContext.Session.GetString(SessionKeyList) != null)
            {
                string jsonP = HttpContext.Session.GetString(SessionKeyList);
                listaProductos = JsonSerializer.Deserialize<List<Producto>>(jsonP);
            }

            return View(listaProductos);
        }


    }
}
