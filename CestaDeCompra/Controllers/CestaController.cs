using CestaDeCompra.Models;
using CestaDeCompra.Repository;
using CestaDeCompra.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CestaDeCompra.Controllers
{
    public class CestaController : Controller
    {

        /// <summary>
        /// Puede crear una cesta si no existe y añadir un producto a la cesta
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Guardar(string productCode)
        {

            if (SessionUtils.GetSessionUsuari(HttpContext) != null)
            {
                Usuari? user = SessionUtils.GetSessionUsuari(HttpContext);
                if (user.IsAdmin) return LocalRedirect("/home/index");

            }
            ProductoRepository prodRepo = new();
            List<Producto> listaProductos = prodRepo.GetProductos();

            // Recupero la cesta, si no hay ninguna me retorna un cesta vacia
            Cesta? cestaProductos = SessionUtils.GetSessionCesta(HttpContext);
            cestaProductos.AddProducto(productCode);

            SessionUtils.SetSessionCesta(HttpContext, cestaProductos);

            return LocalRedirect("/home/index");
        }

        /// <summary>
        /// Recupera y envia la cesta a la vista
        /// </summary>
        /// <returns></returns>
        public IActionResult Cesta()
        {

            var cestaProductos = SessionUtils.GetSessionCesta(HttpContext);

            return View(cestaProductos);
        }

        /// <summary>
        /// Recupera la cesta, actualiza la cantidad del producto determinaro
        /// y la guarda en la sesion nuevamente
        /// </summary>
        /// <param name="nuevaCantidad"></param>
        /// <param name="producCode"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Actualizar(string nuevaCantidad, string producCode)
        {
            Cesta? cestaProductos = SessionUtils.GetSessionCesta(HttpContext);

            cestaProductos.UpdateCantidadProd(producCode, Convert.ToInt32(nuevaCantidad));
            SessionUtils.SetSessionCesta(HttpContext, cestaProductos);

            return LocalRedirect("/cesta/Cesta");
        }

        /// <summary>
        ///  Elimina la cesta antes de ir a la factura
        /// </summary>
        /// <returns></returns>
        public IActionResult Compra()
        {
            var cestaProductos = SessionUtils.GetSessionCesta(HttpContext);
            SessionUtils.DeleteSessionCesta(HttpContext);

            return View(cestaProductos);
        }

    }
}
