using System.ComponentModel.DataAnnotations;

namespace CestaDeCompra.Models
{
    public class Producto
    {

        [Required(ErrorMessage = "Ingrese el código del producto")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre del producto")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese el precio del producto")]
        public double Precio { get; set; }

        public string ImgNom {  get; set; }
    }
}
