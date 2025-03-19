using CestaDeCompra.Models;

namespace CestaDeCompra.Repository.interfaces
{
    public interface IProductoRepository
    {
        void AddProducto(Producto p);
        void DeleteProducto(Producto p);
        void DeleteProducto(string cod);
        bool ExistProducto(string cod);
        Producto? GetProducto(string cod);
        List<Producto> GetProductos();
    }
}