using APIRestaurante.Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace APIRestaurante.Repositories.Repositories
{
    public class CardapioRepository(IConfiguration configuration) : Context(configuration)
    {
        public void C_Inserir(ItemCardapio item)
        {
            string comandoSql = @"INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES (@Nome, @Categoria, @IngredientesDesc, @Valor);";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Nome", item.Nome);
                cmd.Parameters.AddWithValue("@Categoria", item.Categoria);
                cmd.Parameters.AddWithValue("@IngredientesDesc", item.IngredientesDesc);
                cmd.Parameters.AddWithValue("@Valor", item.Valor);
                cmd.ExecuteNonQuery();
            }
        }

        public List<ItemCardapio> R_Listar_Cardapio(string? offset)
        {
            string comandoSql = @"SELECT * FROM Cardapio";

            if (!string.IsNullOrEmpty(offset)) comandoSql += " OFFSET @offset;";
            else comandoSql += ";";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                if (offset != null) cmd.Parameters.AddWithValue("@offset", offset);

                using (var rdr = cmd.ExecuteReader())
                {

                    var cardapio = new List<ItemCardapio>();
                    while (rdr.Read())
                    {
                        var item = new ItemCardapio
                        {
                            IdItem = Convert.ToUInt32(rdr["idItem"]),
                            Nome = rdr["Nome"] as string ?? string.Empty,
                            Categoria = Convert.ToUInt16(rdr["Categoria"]),
                            IngredientesDesc = rdr["IngredientesDesc"] as string ?? string.Empty,
                            Valor = Convert.ToDecimal(rdr["Valor"])
                        };
                        cardapio.Add(item);
                    }
                    return cardapio;
                }
            }
        }

        public ItemCardapio R_Obter_Item(string idItem)
        {
            string comandoSql = @"SELECT * FROM CardapioItemCardapios WHERE idItem = @idItem;";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@idItem", idItem);

                using (var rdr = cmd.ExecuteReader())
                {

                        var item = new ItemCardapio
                        {
                            IdItem = Convert.ToUInt32(rdr["idItem"]),
                            Nome = rdr["Nome"] as string ?? string.Empty,
                            IngredientesDesc = rdr["IngredientesDesc"] as string ?? string.Empty,
                            Valor = Convert.ToDecimal(rdr["Valor"])
                        };
                        return item;
                }
            }
        }


        public void VerificarSeExiste(ItemCardapio item)
        {
            string comandoSql = @"SELECT * FROM Cardapio WHERE Nome = @Nome";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Nome", item.Nome);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum prato encontrado de nome @Nome");
            }
        }

        public void UpdateAtualizar(ItemCardapio item)
        {
            string comandoSql = @"UPDATE Cardapio SET Nome = @Nome, IngredientesDesc = @IngredientesDesc, Valor = @Valor WHERE idItem = @idItem;";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@idItem", item.IdItem);
                cmd.Parameters.AddWithValue("@Nome", item.Nome);
                cmd.Parameters.AddWithValue("@IngredientesDesc", item.IngredientesDesc);
                cmd.Parameters.AddWithValue("@Valor", item.Nome);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro atualizado para o id @idItem no cardápio");
            }
        }

        public void DeleteDeletar(string idItemCardapio)
        {
            string comandoSql = @"DELETE FROM Cardapio WHERE idItem = @idItem";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@idItem", idItemCardapio);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro deletado para o id @idItem do cardápio");
            }
        }

    }
}
