using Microsoft.AspNetCore.Mvc;
using CestaDeCompra.Models;
using CestaDeCompra.Data;
using System.Data;
using System.Text.Json;
using Microsoft.AspNetCore.Routing.Constraints;
using CestaDeCompra.Utils;
using CestaDeCompra.Repository.interfaces;

namespace CestaDeCompra.Controllers
{
    public class ProductoController : Controller
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductoRepository _productoRepository;

        public ProductoController(IWebHostEnvironment webHostEnvironment, IProductoRepository productoRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _productoRepository = productoRepository;
        }
        /// <summary>
        /// Retorna la vista que contiene el formulario para añadir un producto.
        /// Si el usuario no es admin, lo redirije al home.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Agregar()
        {
            // Si hay un usuario en la session verifica si es admin
            if (SessionUtils.GetSessionUsuari(HttpContext) != null)
            {
                Usuari? user = SessionUtils.GetSessionUsuari(HttpContext);
                // Si no lo es, se redirígelo
                if (!user.IsAdmin) return LocalRedirect("/home/index");
            } else // Si no hay usuario en la sesion, redigir también
                return LocalRedirect("/home/index");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(Producto p, IFormFile ImgNom)
        {

            if (_productoRepository.ExistProducto(p.Codigo)) {
                ModelState.AddModelError("Codigo", "Código de poducto ya existente");
                return View(p); 
            }

            ModelState.Clear();
            // Console.WriteLine(p.Precio +  p.Codigo + p.Nombre);
            if (TryValidateModel(p))
            {
                
                p.ImgNom = await OnPostUploadAsync(ImgNom);
                _productoRepository.AddProducto(p);
            }
            return LocalRedirect("/home/index");
        }

        public async Task<string> OnPostUploadAsync(IFormFile file)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string fileName = "";
            string extension = "";

            if (file.Length > 0)
            {
                fileName = Guid.NewGuid().ToString();
                extension = Path.GetExtension(file.FileName);

                var filePath = Path.Combine(webRootPath, "Img", fileName+extension);
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
