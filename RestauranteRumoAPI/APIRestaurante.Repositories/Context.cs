using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace APIRestaurante.Repositories
{
    public class Context
    {
        internal readonly MySqlConnection _conn;

        public Context(IConfiguration configuration)
        {
            _conn = new MySqlConnection(configuration["DbString"]);
        }

        public void AbrirConexao()
        {
            _conn.Open();
        }

        public void FecharConexao()
        {
            _conn.Close();
        }
    }
}
