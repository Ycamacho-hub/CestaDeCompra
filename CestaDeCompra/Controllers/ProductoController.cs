using Microsoft.AspNetCore.Mvc;
using CestaDeCompra.Models;
using CestaDeCompra.Data;
using System.Data;
using System.Text.Json;
using Microsoft.AspNetCore.Routing.Constraints;

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
            List<Producto> listaProductos = prodRepo.GetProductos();
            // Dictionary<string, int> cestaProductos = [];
            string jsonCesta;
            int numCompra;

            Cesta cestaProductos = new();

            if (HttpContext.Session.GetInt32(SessionKeyBuy) != null)
            {
                jsonCesta = HttpContext.Session.GetString(SessionKeyList);
                cestaProductos = JsonSerializer.Deserialize<Cesta>(jsonCesta);

                cestaProductos.AddProducto(productCode);
            }
            else
            {
                cestaProductos.AddProducto(productCode);
            }

            numCompra = cestaProductos.GetProductosTotales();
            jsonCesta = JsonSerializer.Serialize<Cesta>(cestaProductos);

            HttpContext.Session.SetInt32(SessionKeyBuy, numCompra);
            HttpContext.Session.SetString(SessionKeyList, jsonCesta);

            return LocalRedirect ("/producto/index");
        }

        public IActionResult Cesta()
        {
            var cestaProductos = new Cesta();

            if(HttpContext.Session.GetString(SessionKeyList) != null)
            {
                string jsonP = HttpContext.Session.GetString(SessionKeyList);
                cestaProductos = JsonSerializer.Deserialize<Cesta>(jsonP);
            }

            return View(cestaProductos);
        }

        [HttpGet]
        public IActionResult Actualizar(string nuevaCantidad, string producCode)
        {
            Cesta cestaProductos = new();
            string jsonCesta;
            int numCompra;


            if (HttpContext.Session.GetInt32(SessionKeyBuy) != null)
            {
                jsonCesta = HttpContext.Session.GetString(SessionKeyList);
                cestaProductos = JsonSerializer.Deserialize<Cesta>(jsonCesta);

                cestaProductos.UpdateCantidadProd(producCode, Convert.ToInt32(nuevaCantidad));
            }

            numCompra = cestaProductos.GetProductosTotales();
            jsonCesta = JsonSerializer.Serialize(cestaProductos);

            HttpContext.Session.SetInt32(SessionKeyBuy, numCompra);
            HttpContext.Session.SetString(SessionKeyList, jsonCesta);

            return LocalRedirect("/producto/Cesta");
        }

        public IActionResult Compra()
        {
            var cestaProductos = new Cesta();

            if (HttpContext.Session.GetString(SessionKeyList) != null)
            {
                string jsonP = HttpContext.Session.GetString(SessionKeyList);
                cestaProductos = JsonSerializer.Deserialize<Cesta>(jsonP);

                HttpContext.Session.Remove(SessionKeyBuy);
                HttpContext.Session.Remove(SessionKeyList);
            }

            return View(cestaProductos);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(Producto p, IFormFile ImgNom)
        {

            if(prodRepo.GetProducto(p.Codigo) != null) {
                ViewData["ErrorCode"] = "Ya existe un producto con este codigo";
                return View(p); 
            }

            ModelState.Clear();
            // Console.WriteLine(p.Precio +  p.Codigo + p.Nombre);
            if (TryValidateModel(p))
            {
                
                p.ImgNom = await OnPostUploadAsync(ImgNom);
                prodRepo.AddProducto(p);
            }
            return LocalRedirect("/producto/index");
        }

        public async Task<string> OnPostUploadAsync(IFormFile file)
        {
            string fileName = "";
            string extension = "";

            if (file.Length > 0)
            {
                fileName = Guid.NewGuid().ToString();
                extension = Path.GetExtension(file.FileName);

                var filePath = Path.Combine("C:\\Users\\ycamacho\\Desktop\\Net-WorkSpace\\CestaDeCompra\\CestaDeCompra\\wwwroot\\Img\\"+fileName+extension);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
                
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return fileName + extension;
        }



    }
}
