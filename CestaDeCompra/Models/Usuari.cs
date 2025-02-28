using System.ComponentModel.DataAnnotations;

namespace CestaDeCompra.Models
{
    public class Usuari
    {
        /// <summary>email. Serveix com a login. Ha de ser únic.</summary>
        [Required(ErrorMessage = "Ingrese un correo electrónico")]
        public string Email { get; set; }

        /// <summary>Password. Es guarda sense cap encriptaió</summary>
        [Required(ErrorMessage = "Ingrese la contraseña")]
        public string Password { get; set; }
        
        /// <summary>Si es true es un administrador.</summary>
        public bool IsAdmin { get; set; }

        /// <summary>Si es true, l'usuari esta bloquejat i no pot fer login</summary>
        public bool Locked { get; set; }

        /// <summary>Data y hora de creació o darrera edició o bloqueig</summary>
        public DateTime Lastupdate { get; set; }

        public Usuari() {
            this.Email = string.Empty;
            this.Password = string.Empty;
        } 

        public Usuari(String password, string email)
        {
            this.Password = password;  
            this.Email = email;
        }
        public Usuari(string password, string email, bool isAdmin, bool locked, DateTime lastupdate)
        {
            this.Password = password;
            this.Email = email;
            this.IsAdmin = isAdmin;
            this.Locked = locked;
            this.Lastupdate = lastupdate;
        }
    }
}
