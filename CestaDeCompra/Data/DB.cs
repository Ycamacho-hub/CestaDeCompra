using MySql.Data.MySqlClient;

namespace CestaDeCompra.Data
{
    public static class DB
    {
        public static MySqlConnection? Open()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = "server=127.0.0.1;uid=root;pwd=8335;database=pixelstore";
            conn.Open();

            return conn;
        }
    }
}
