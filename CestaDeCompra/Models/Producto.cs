using System.ComponentModel.DataAnnotations;

namespace CestaDeCompra.Models
{
    public class Producto
    {
        [Key]
        [Required(ErrorMessage = "Ingrese el código del producto")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre del producto")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese el precio del producto")]
        public double Precio { get; set; }

        public string? ImgNom {  get; set; }

        public Producto() 
        {
            this.Codigo = string.Empty;
            this.Nombre = string.Empty;
            this.Precio = 0;
            this.ImgNom = string.Empty;
        }
        public Producto(string codigo, string nombre, double precio, string imgNom)
        {
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Precio = precio;
            this.ImgNom = imgNom;
        }
    }
}
