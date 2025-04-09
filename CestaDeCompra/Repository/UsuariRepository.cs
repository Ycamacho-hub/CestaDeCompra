using CestaDeCompra.Models;
using CestaDeCompra.Data;
using CestaDeCompra.Repository.interfaces;

namespace CestaDeCompra.Repository
{
    public class UsuariRepository : IUsuariRepository
    {
        /// <summary>
        /// Método que añade un usuario a la lista
        /// </summary>
        /// <param name="user"></param>
        public void CreateUsuari(UsuariLogin user)
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
            foreach (UsuariLogin us in Usuaris._usuaris)
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
            Usuaris._usuaris.ForEach(us =>
            {
                if (us.Email.Equals(email)) passw = us.Password; ;
            });
            return passw;
        }

        public bool CheckOutUsuari(UsuariLogin us)
        {
            UsuariLogin user = GetUsuari(us.Email);
            if (ExistUsuari(us.Email) && us.Password.Equals(user.Password) && user.Locked == false)
                return true;
            return false;
        }

        /// <summary>
        /// Método que retorna un usuario determinado según el imail pasado.
        /// O null si el  el usuario no existe
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UsuariLogin? GetUsuari(string email)
        {
            UsuariLogin? user = null;
            Usuaris._usuaris.ForEach((us) =>
            {
                if (us.Email.Equals(email)) user = us;
            });

            return user;
        }

        public void DeleteUsuari(string email)
        {
            Usuaris._usuaris.RemoveAll(user => user.Email.Equals(email));
        }

        /// <summary>
        /// Bloquea el usuario con el email especificado
        /// </summary>
        /// <param name="email"></param>
        public void BlockUsuari(string email)
        {
            UsuariLogin user = GetUsuari(email);
            user.Locked = true;
            DeleteUsuari(email);
            CreateUsuari(user);
        }


        /// <summary>
        /// Bloquea el usuario con el email especificado
        /// </summary>
        /// <param name="email"></param>
        public void UnBlockUsuari(string email)
        {
            UsuariLogin user = GetUsuari(email);
            user.Locked = false;
            DeleteUsuari(email);
            CreateUsuari(user);
        }

    }
}
