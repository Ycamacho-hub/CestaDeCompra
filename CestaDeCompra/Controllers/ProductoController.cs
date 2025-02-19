﻿using Microsoft.AspNetCore.Mvc;
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

        public int GetNumCompra(Dictionary<string, int> d)
        {
            int numCompra = 0;
            foreach(KeyValuePair<string, int> kvp in d)
            {
                numCompra += kvp.Value;
            }

            return numCompra;
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

                //copiaCesta = cestaProductos;
                //InicializarCesta(copiaCesta);
                
                //jsonP = JsonSerializer.Serialize(copiaCesta);
                //HttpContext.Session.SetString(SessionKeyList, jsonP);
                //HttpContext.Session.SetInt32(SessionKeyBuy, GetNumCompra(copiaCesta));
                HttpContext.Session.Remove(SessionKeyBuy);
                HttpContext.Session.Remove(SessionKeyList);
            }

            return View(cestaProductos);
        }

        public Dictionary<string, int> InicializarCesta(Dictionary<string, int> cesta)
        {
            foreach(KeyValuePair<string, int> kvp in cesta)
            {
                cesta[kvp.Key] = 0;
            }

            return cesta;
        }

    }
}
