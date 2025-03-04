using APIRestaurante.Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace APIRestaurante.Repositories.Repositories
{
    public class ClienteRepository(IConfiguration configuration) : Context(configuration)
    {
        public long C_Inserir(Cliente cliente)
        {
            string comandoSql = @"INSERT INTO Clientes (Nome, Mesa, Data, Fechado) VALUES (@Nome, @Mesa, @Data, @Fechado);";

            using var cmd = new MySqlCommand(comandoSql, _conn);
            cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@Mesa", cliente.Mesa);
            cmd.Parameters.AddWithValue("@Data", cliente.Data);
            cmd.Parameters.AddWithValue("@Fechado", cliente.Fechado);
            cmd.ExecuteNonQuery();
            return cmd.LastInsertedId;
        }
    }
}