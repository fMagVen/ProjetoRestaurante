using APIRestaurante.Domain.Exceptions;
using APIRestaurante.Domain.Models;
using APIRestaurante.Repositories.Repositories;

namespace APIRestaurante.Services
{
    public class ClienteService(ClienteRepository repositorio)
    {
        public long Criar(Cliente cliente)
        {
            try
            {
                ValidarInserir(cliente);
                repositorio.AbrirConexao();
                return repositorio.C_Inserir(cliente);
            }
            finally
            {
                repositorio.FecharConexao();
            }
            
        }

        private static void ValidarInserir(Cliente cliente)
        {
            if (cliente == null) throw new ValidacaoException("JSON mal formatado ou vazio.");
            if (string.IsNullOrEmpty(cliente.Nome)) throw new ValidacaoException("O cliente deve ter um nome.");
            if (cliente.Nome.Length < 2) throw new ValidacaoException("O tamanho mínimo do nome é 2 caracteres.");
            if (cliente.Nome.Length > 80) throw new ValidacaoException("O tamanho máximo de um nome é 80 caracteres.");
            if (cliente.Mesa > 999) throw new ValidacaoException("O número máximo da mesa deve ser 999.");
        }
    }
}