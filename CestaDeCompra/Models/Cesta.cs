using System.Text.Json.Serialization;

namespace CestaDeCompra.Models
{
    public class Cesta
    {

        [JsonInclude]
        private Dictionary<string, int> CestaProductos { get; set; } = [];

        // Constructores
        public Cesta(Dictionary<string, int> cesta) {
            SetCesta(cesta);
        }
        public Cesta() { }

        // Métodos

        /// <summary>
        /// Añade un producto a la cesta, si este ya existe le aumenta en 1
        /// </summary>
        /// <param name="codProducto"></param>
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


        /// <summary>
        /// Actualiza la cantidad de productos por la que se indica.
        /// Si la cantidad == 0, se elimina el producto de la Cesta
        /// </summary>
        /// <param name="codProducto"></param>
        /// <param name="cantidad"></param>
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
        /// Retorna el total de todas las cantidades sumadas de los productos.
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

        /// <summary>
        /// Guarda una Dictionary<string, int> dentro de la cesta.
        /// </summary>
        /// <param name="cesta"></param>
        public void SetCesta(Dictionary<string, int> cesta)
        {
            this.CestaProductos = cesta;
        }

        /// <summary>
        /// Retrona la Cesta, dentro de esta hay un diccionario donde la clave de 
        /// cada elemento es el código del producto y los valores son las cantidades
        /// del mismo.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetCesta()
        {
            return this.CestaProductos;
        }

    }
}
