using APIRestaurante.Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace APIRestaurante.Repositories.Repositories
{
    public class ColaboradorRepository(IConfiguration configuration) : Context(configuration)
    {

        public long C_Cadastrar(Colaborador colaborador)
        {
            string comandoSql = @"INSERT INTO Colaboradores (idSetor, Nome, Senha) VALUES (@IdSetor, @Nome, @Senha);";

            using var cmd = new MySqlCommand(comandoSql, _conn);
            cmd.Parameters.AddWithValue("@idSetor", colaborador.IdSetor);
            cmd.Parameters.AddWithValue("@Nome", colaborador.Nome);
            cmd.Parameters.AddWithValue("@Senha", colaborador.Senha);
            cmd.ExecuteNonQuery();
            return cmd.LastInsertedId;
        }

        public bool R_ValidarLogin(Colaborador Colaborador)
        {
            string comandoSql = @"SELECT idColaborador, Senha FROM Colaboradores WHERE idColaborador = @IdColaborador AND Senha = @Senha;";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdColaborador", Colaborador.IdColaborador);
                cmd.Parameters.AddWithValue("@Senha", Colaborador.Senha);

                using (var rdr = cmd.ExecuteReader())
                {

                    if (rdr.HasRows) return true;
                    else return false;
                }
            }
        }

        public Colaborador R_Logar(Colaborador Colaborador)
        {
            string comandoSql = @"SELECT Colaboradores.Nome, Colaboradores.idSetor, Setores.Nome AS Setor FROM Colaboradores LEFT JOIN Setores ON Colaboradores.idSetor = Setores.idSetor WHERE idColaborador = @IdColaborador;";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdColaborador", Colaborador.IdColaborador);

                using var rdr = cmd.ExecuteReader();
                rdr.Read();
                var colaborador = new Colaborador
                {
                    Nome = rdr["Nome"] as string ?? string.Empty,
                    IdSetor = Convert.ToUInt16(rdr["idSetor"]),
                    NomeSetor = rdr["Setor"] as string ?? string.Empty
                };
                return colaborador;
            }
        }

        public UInt16? R_ValidarConsulta(UInt16? IdAdmin)
        {
            string comandoSql = @"SELECT Setor FROM Colaboradores WHERE idColaborador = @IdAdmin;";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdAdmin", IdAdmin);

                using (var rdr = cmd.ExecuteReader())
                {

                    if (rdr.Read())
                    {
                        UInt16 setor = Convert.ToUInt16(rdr["Setor"]);
                        return setor;
                    }
                    else return null;
                }
            }
        }

        public Colaborador? R_Consultar(Colaborador Colaborador)
        {
            string comandoSql = @"SELECT idColaborador, Colaboradores.idSetor, Nome, Setor.Nome as Setor FROM Colaboradores LEFT JOIN Setores ON Colaboradores.idSetor = Setores.idSetor";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                if (!string.IsNullOrEmpty(Colaborador.Nome))
                {
                    cmd.Parameters.AddWithValue("@Nome", Colaborador.Nome);
                    comandoSql += " WHERE Nome = @Nome;";
                }else if (!string.IsNullOrEmpty(Colaborador.NomeSetor))
                {
                    cmd.Parameters.AddWithValue("@Setor", Colaborador.NomeSetor);
                    comandoSql += " WHERE Setor = @Setor;";
                }

                    using (var rdr = cmd.ExecuteReader())
                {

                    if (rdr.Read())
                    {
                        var colaborador = new Colaborador
                        {
                            IdColaborador = Convert.ToUInt32(rdr["idColaborador"]),
                            Nome = rdr["Nome"] as string ?? string.Empty,
                            IdSetor = Convert.ToUInt16(rdr["Setor"]),
                            NomeSetor = rdr["Setor"] as string ?? string.Empty
                        };
                        return colaborador;
                    }
                    else return null;
                }
            }
        }

        public List<Setor> R_LerSetores()
        {
            string comandoSql = @"SELECT idSetor, Nome FROM Setores;";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {

                using (var rdr = cmd.ExecuteReader())
                {

                    var setores = new List<Setor>();
                    while (rdr.Read())
                    {
                        var setor = new Setor
                        {
                            IdSetor = Convert.ToUInt16(rdr["idSetor"]),
                            Nome = rdr["Nome"] as string ?? string.Empty,
                        };
                        setores.Add(setor);
                    }
                    return setores;
                }
            }
        }

        public bool U_Atualizar(Colaborador colaborador)
        {
            string comandoSql = @"UPDATE Colaborador SET";
            if (!string.IsNullOrEmpty(colaborador.Nome)) comandoSql += " Nome = @Nome,";
            if (!string.IsNullOrEmpty(colaborador.Senha)) comandoSql += " Senha = @Senha,";
            if (colaborador.IdSetor.HasValue) comandoSql += " idSetor = @IdSetor";
            comandoSql += " WHERE idColaborador = @IdColaborador;";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                if (!string.IsNullOrEmpty(colaborador.Nome))
                    cmd.Parameters.AddWithValue("@Nome", colaborador.Nome);
                if (!string.IsNullOrEmpty(colaborador.Senha))
                    cmd.Parameters.AddWithValue("@Senha", colaborador.Senha);
                if (colaborador.IdSetor.HasValue)
                    cmd.Parameters.AddWithValue("@IdSetor", colaborador.IdSetor);
                cmd.Parameters.AddWithValue("@IdColaborador", colaborador.IdColaborador);
                if (cmd.ExecuteNonQuery() == 0)
                    return false;
                else return true;
            }
        }

        public bool D_Desligar(UInt32? IdColaborador)
        {
            string comandoSql = @"DELETE FROM Colaboradores WHERE idColaborador = @IdColaborador";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdColaborador", IdColaborador);
                if (cmd.ExecuteNonQuery() == 0)
                    return false;
                else return true;
            }
        }
    }
}
