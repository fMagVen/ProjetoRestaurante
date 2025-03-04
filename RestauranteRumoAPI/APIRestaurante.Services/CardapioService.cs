using APIRestaurante.Domain.Exceptions;
using APIRestaurante.Domain.Models;
using APIRestaurante.Repositories.Repositories;

namespace APIRestaurante.Services
{
    public class CardapioService(CardapioRepository repositorio)
    {
        public void Criar(ItemCardapio item)
        {
            try
            {
                ValidarInserir(item);
                repositorio.AbrirConexao();
                repositorio.C_Inserir(item);
            }
            finally
            {
                repositorio.FecharConexao();
            }
        }

        public List<ItemCardapio> Listar_n(string? offset)
        {
            try
            {
                repositorio.AbrirConexao();
                return repositorio.R_Listar_Cardapio(offset);
            }
            finally
            {
                repositorio.FecharConexao();
            }
        }

        public void Atualizar(ItemCardapio model)
        {
            try
            {
                repositorio.AbrirConexao();
                repositorio.UpdateAtualizar(model);
            }
            finally
            {
                repositorio.FecharConexao();
            }
        }

        public void Deletar(string idItemCardapio)
        {
            try
            {
                repositorio.AbrirConexao();
                repositorio.DeleteDeletar(idItemCardapio);
            }
            finally
            {
                repositorio.FecharConexao();
            }
        }

        private static void ValidarInserir(ItemCardapio prato)
        {
            if (prato == null) throw new ValidacaoException("JSON mal formatado ou vazio.");
            if (string.IsNullOrEmpty(prato.Nome)) throw new ValidacaoException("O prato deve ter um nome.");
            if (string.IsNullOrEmpty(prato.IngredientesDesc)) throw new ValidacaoException("O prato deve ter uma descrição para leitura do cliente.");
            if (decimal.IsNegative(prato.Valor)) throw new ValidacaoException("O valor inserido para o prato é negativo.");
        }
    }
}
