using CestaDeCompra.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CestaDeCompra.Data
{
    public static class BDproducto
    {
        public static List<Producto> productos = new();


        static BDproducto()
        {
            productos.Add(new Producto() { Codigo = "A123", Nombre = "Cactus", Precio = 15.90, ImgNom = "Cactus.png" });
            productos.Add(new Producto() { Codigo = "B456", Nombre = "GameBoy", Precio = 75.20, ImgNom = "Gameboy.png" });
            productos.Add(new Producto() { Codigo = "C451", Nombre = "Manzana", Precio = 2.80, ImgNom = "Manzana.png" });
            productos.Add(new Producto() { Codigo = "A117", Nombre = "Pecera", Precio = 10.00, ImgNom = "Pecera.png" });
            productos.Add(new Producto() { Codigo = "F458", Nombre = "Rana", Precio = 7.40, ImgNom = "Rana.png" });

        }


        //public static void Add(Producto p)
        //{
        //    productos.Add(new Producto() { Codigo = p.Codigo, Nombre = p.Nombre, Precio = p.Precio, ImgNom = p.ImgNom });
        //}

    }
}
