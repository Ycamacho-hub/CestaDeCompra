using CestaDeCompra.Models;
using CestaDeCompra.Data;

namespace CestaDeCompra.Repository
{
    public class ProductoRepository
    {
        public Producto? GetProducto(string cod)
        {
            foreach (Producto p in BDproducto.productos)
            {
                if (p.Codigo == cod) return p;
            }

            return null;
        }

        public bool ExistProducto(string cod)
        {
            return GetProducto(cod) != null;
        }

        public List<Producto> GetProductos()
        {
            return BDproducto.productos;
        }

        public void AddProducto(Producto p)
        {
            BDproducto.productos.Add(p);
        }

        public void DeleteProducto(String cod)
        {
            Producto? p = GetProducto(cod);
            if (p != null) BDproducto.productos.Remove(p);
        }

        public void DeleteProducto(Producto p)
        {
            BDproducto.productos.Remove(p);
        }

    }
}
