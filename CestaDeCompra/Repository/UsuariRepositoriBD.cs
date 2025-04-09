using CestaDeCompra.Models;
using CestaDeCompra.Data;
using CestaDeCompra.Repository.interfaces;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace CestaDeCompra.Repository
{
    public class UsuariRepositoriBD : IUsuariRepository
    {

        MySqlConnection? connection;
        MySqlTransaction? trans;

        public UsuariRepositoriBD(MySqlConnection connection, MySqlTransaction trans)
        {
            this.connection = connection;
            this.trans = trans;
        }

        public void BlockUsuari(string email)
        {
            throw new NotImplementedException();
        }

        public bool CheckOutUsuari(UsuariLogin us)
        {
            MySqlConnection? conn = DB.Open();
            if (conn == null) return false;

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Count(*) FROM users WHERE email = @email AND password = @password";
            cmd.Parameters.AddWithValue("@email", us.Email);
            cmd.Parameters.AddWithValue("@password", us.Password);
            long num = (long) cmd.ExecuteScalar();
            conn.Close();

            return num > 0;
        }

        public void CreateUsuari(UsuariLogin user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUsuari(string email)
        {
            throw new NotImplementedException();
        }

        public bool ExistUsuari(string email)
        {
            throw new NotImplementedException();
        }

        public string GetPasswUsuari(string email)
        {
            throw new NotImplementedException();
        }

        public UsuariLogin? GetUsuari(string email)
        {
            UsuariLogin? usuari = null;
            string query = "SELECT password, isAdmin, locked, lastUpdate FROM users WHERE email = @email";
            MySqlConnection? conn = DB.Open();
            if (conn == null) return null;

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@email", email);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                usuari = new UsuariLogin();
                usuari.Email = email;
                usuari.Password = reader.GetString("password");
                usuari.IsAdmin = reader.GetBoolean("isAdmin");
                usuari.Locked = reader.GetBoolean("locked");

                if (!reader.IsDBNull(reader.GetOrdinal("lastUpdate")))
                {
                    usuari.Lastupdate = reader.GetDateTime("lastUpdate");
                }
            }
            conn.Close();
            return usuari;
        }

        public void UnBlockUsuari(string email)
        {
            throw new NotImplementedException();
        }
    }
}
