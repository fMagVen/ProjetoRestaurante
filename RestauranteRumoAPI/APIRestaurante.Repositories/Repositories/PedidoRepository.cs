using APIRestaurante.Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace APIRestaurante.Repositories.Repositories
{
    public class PedidoRepository(IConfiguration configuration) : Context(configuration)
    {
        public void C_Inserir(Pedido pedido)
        {
            string comandoSql = @"INSERT INTO Pedidos (idItem, idCliente, Quantidade, Status) VALUES (@idItem, @idCliente, @Quantidade, @Status);";

            using var cmd = new MySqlCommand(comandoSql, _conn);
            cmd.Parameters.AddWithValue("@idItem", pedido.IdItem);
            cmd.Parameters.AddWithValue("@idCliente", pedido.IdCliente);
            cmd.Parameters.AddWithValue("@Quantidade", pedido.Quantidade);
            cmd.Parameters.AddWithValue("@Status", pedido.Status);
            cmd.ExecuteNonQuery();
        }

        public List<Controle> R_ChecarColaborador(UInt16 setor)
        {
            string comandoSql = @"SELECT idPedido, Cardapio.idItem, Cardapio.Nome AS NomeItem, Categoria, Status, Quantidade";
            if (setor == 4)
                comandoSql += @", Clientes.Nome AS NomeCliente, Mesa";
            comandoSql += @" FROM Pedidos LEFT JOIN Cardapio ON Pedidos.idItem = Cardapio.idItem LEFT JOIN Clientes ON Clientes.idCliente = Pedidos.idCliente";
                
            if (setor == 2) comandoSql += " WHERE Categoria IN (1, 2, 3, 4, 5, 6)"; //cozinha
            if (setor == 3) comandoSql += " WHERE Categoria IN (7, 8)"; //copa
            if (setor == 4) comandoSql += " WHERE Status = 3"; //garçon, pronto

            comandoSql += " ORDER BY Status DESC;";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                using (var rdr = cmd.ExecuteReader())
                {
                    var pedidosControle = new List<Controle>();
                    while (rdr.Read())
                    {
                        var controle = new Controle
                        {
                            IdPedido = Convert.ToUInt64(rdr["idPedido"]),
                            IdItem = Convert.ToUInt32(rdr["idItem"]),
                            NomeItem = rdr["NomeItem"] as string ?? string.Empty,
                            Categoria = Convert.ToUInt16(rdr["Categoria"]),
                            Status = Convert.ToUInt16(rdr["Status"]),
                            Quantidade = Convert.ToUInt32(rdr["Quantidade"]),
                        };
                        if (setor == 4)
                        {
                            controle.NomeCliente = rdr["NomeCliente"] as string ?? string.Empty;
                            controle.Mesa = Convert.ToUInt16(rdr["Mesa"]);
                        }
                            pedidosControle.Add(controle);
                    }
                    return pedidosControle;
                }
            }
        }

        public List<Controle> R_ChecarCliente(UInt64 IdCliente)
        {
            string comandoSql = @"SELECT idPedido, Cardapio.Nome AS NomeItem, Categoria, Status, Quantidade FROM Pedidos LEFT JOIN Cardapio ON Pedidos.idItem = Cardapio.idItem WHERE idCliente = @IdCliente ORDER BY Status DESC;";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdCliente", IdCliente);
                using (var rdr = cmd.ExecuteReader())
                {
                    var pedidosControle = new List<Controle>();
                    while (rdr.Read())
                    {
                        var controle = new Controle
                        {
                            IdPedido = Convert.ToUInt64(rdr["idPedido"]),
                            IdItem = Convert.ToUInt32(rdr["idItem"]),
                            NomeItem = rdr["NomeItem"] as string ?? string.Empty,
                            Categoria = Convert.ToUInt16(rdr["Categoria"]),
                            Status = Convert.ToUInt16(rdr["Status"]),
                            Quantidade = Convert.ToUInt32(rdr["Quantidade"]),
                        };
                        pedidosControle.Add(controle);
                    }
                    return pedidosControle;
                }
            }
        }

        public void U_AtualizarStatus(Pedido pedido)
        {
            string comandoSql = @"UPDATE Pedidos SET Status = @Status WHERE idPedido = @IdPedido AND idItem = @IdItem;";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdPedido", pedido.IdPedido);
                cmd.Parameters.AddWithValue("@IdItem", pedido.IdItem);
                cmd.Parameters.AddWithValue("@Status", pedido.Status);
                cmd.ExecuteNonQuery();
            }
        }

        public void U_Editar(Pedido pedido)
        {
            string comandoSql = @"INSERT INTO Pedidos (idItem, idCliente, Status) VALUES (@idItem, @idCliente, @Status);";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@idItem", pedido.IdItem);
                cmd.Parameters.AddWithValue("@idCliente", pedido.IdCliente);
                cmd.Parameters.AddWithValue("@Status", pedido.Status);
                cmd.ExecuteNonQuery();
            }
        }

        public bool D_Cancelar(UInt32 IdPedido)
        {
            string comandoSql = @"DELETE FROM Pedidos WHERE idPedido = @idPedido;";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@idPedido", IdPedido);
                int resultado = cmd.ExecuteNonQuery();
                if (resultado > 0) return true; else return false;
            }
        }
    }
}
