using System.Text.Json.Serialization;

namespace CestaDeCompra.Models
{
    public class Cesta
    {
        // Constructores
        public Cesta(Dictionary<string, int> cesta) {
            SetCesta(cesta);
        }
        public Cesta() { }

        [JsonInclude]
        private Dictionary<string, int> CestaProductos { get; set; } = [];

        // Métodos
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

        /// <summary>
        /// Retorna el total de todas las cantidades sumadas de los productos
        /// </summary>
        /// <returns>int</returns>
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
            this.CestaProductos = cesta;
        }

        public Dictionary<string, int> GetCesta()
        {
            return this.CestaProductos;
        }

    }
}
