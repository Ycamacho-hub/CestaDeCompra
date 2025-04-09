using CestaDeCompra.Data;
using CestaDeCompra.Repository.interfaces;
using MySql.Data.MySqlClient;

namespace CestaDeCompra.Repository
{
    public class UOW
    {
        MySqlConnection? connection;
        MySqlTransaction? trans;

        //private IProductoRepository _producteRepositorty;

        public void Open()
        {
            connection = DB.Open();
            if(connection == null) throw new Exception("DB kaput..!");
            trans = connection.BeginTransaction();
        }

        public IProductoRepository Prouctes = new ProductoRepository();
        //public IUsuariRepository Users = new UsuariRepositoriBD(connection, trans);

        public void Commit()
        {
            trans.Commit();
        }

        public void Rollback()
        {
            trans.Rollback();
        }

        public void Close()
        {         
            connection.Close();
        }



    }
}
