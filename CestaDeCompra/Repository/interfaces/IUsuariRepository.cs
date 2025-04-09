using CestaDeCompra.Models;

namespace CestaDeCompra.Repository.interfaces
{
    public interface IUsuariRepository
    {
        void BlockUsuari(string email);
        bool CheckOutUsuari(UsuariLogin us);
        void CreateUsuari(UsuariLogin user);
        void DeleteUsuari(string email);
        bool ExistUsuari(string email);
        string GetPasswUsuari(string email);
        UsuariLogin? GetUsuari(string email);
        void UnBlockUsuari(string email);
    }
}