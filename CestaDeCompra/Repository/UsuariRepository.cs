using CestaDeCompra.Models;
using CestaDeCompra.Data;

namespace CestaDeCompra.Repository
{
    public class UsuariRepository
    {
        /// <summary>
        /// Método que añade un usuario a la lista
        /// </summary>
        /// <param name="user"></param>
        public void CreateUsuari(Usuari user)
        {
            Usuaris._usuaris.Add(user);
        }

        /// <summary>
        /// Método que retorna true o false dependiendo de si exite un usuario
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ExistUsuari(string email)
        {
            bool exist = false;
            //_usuaris.ForEach(us => {
            //    if (us.Equals(email)) exist = true; ;
            //});
            foreach (Usuari us in Usuaris._usuaris)
            {
                if (us.Email.Equals(email)) return true;
            }
            return exist;
        }

        /// <summary>
        /// Método que retorna la clave de determinado usuario
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GetPasswUsuari(string email)
        {
            string passw = "";
            Usuaris._usuaris.ForEach(us => {
                if (us.Email.Equals(email)) passw = us.Password; ;
            });
            return passw;
        }

        public Usuari? GetUsuari(string email)
        {
            Usuari? user = null;
            Usuaris._usuaris.ForEach((us) =>
            {
                if (us.Email.Equals(email)) user = us;
            });

            return user;
        }
    }
}
