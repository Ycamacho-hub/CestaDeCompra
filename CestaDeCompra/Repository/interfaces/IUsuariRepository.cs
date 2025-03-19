using CestaDeCompra.Models;

namespace CestaDeCompra.Repository.interfaces
{
    public interface IUsuariRepository
    {
        void BlockUsuari(string email);
        bool CheckOutUsuari(Usuari us);
        void CreateUsuari(Usuari user);
        void DeleteUsuari(string email);
        bool ExistUsuari(string email);
        string GetPasswUsuari(string email);
        Usuari? GetUsuari(string email);
        void UnBlockUsuari(string email);
    }
}