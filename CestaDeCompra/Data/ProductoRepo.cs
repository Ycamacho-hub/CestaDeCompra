using CestaDeCompra.Models;

namespace CestaDeCompra.Data
{
    public class ProductoRepo
    {
        public Producto? GetProducto(string cod)
        {
            foreach(Producto p in BDproducto.productos)
            {
                if(p.Codigo == cod) return p;
            }

            return null;
        }

        public List<Producto> GetProductos()
        {
            return BDproducto.productos;
        }

        public void AddProducto(Producto p)
        {
            BDproducto.Add(p);
        }



    }
}
