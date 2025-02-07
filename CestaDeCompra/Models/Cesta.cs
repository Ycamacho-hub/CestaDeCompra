namespace CestaDeCompra.Models
{
    public class Cesta
    {

        public Cesta() { }
        public Cesta(Dictionary<string, int> cesta) {
            SetCesta(cesta);
        }

        private Dictionary<string, int> CestaProductos = [];

        public void AddProducto(string codProducto)
        {
            if (!CestaProductos.ContainsKey(codProducto))
            {
                CestaProductos[codProducto] = 1;
            } 
            else
            {
                CestaProductos[codProducto]++;
            }
        }

        public void UpdateCantidadProd(string codProducto, int cantidad)
        {
            if (cantidad == 0)
            {
                CestaProductos.Remove(codProducto);
            }
            else
            {
                CestaProductos[codProducto] = cantidad;
            }
        }

        public int GetProductosTotales()
        {
            int total = 0;
            foreach(KeyValuePair<string, int> kvp in this.CestaProductos)
            {
                total += kvp.Value;
            }
            return total;
        }

        public void SetCesta(Dictionary<string, int> cesta)
        {
            CestaProductos = cesta;
        }

    }
}
