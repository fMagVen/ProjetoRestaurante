using APIRestaurante.Domain.Exceptions;
using APIRestaurante.Domain.Models;
using APIRestaurante.Repositories.Repositories;

namespace APIRestaurante.Services
{
    public class PedidoService(PedidoRepository repositorio)
    {
        public void Criar(List<Pedido> pedidos)
        {
            try
            {
                repositorio.AbrirConexao();
                pedidos.ForEach(pedido =>
                {
                    ValidarInserir(pedido);
                    repositorio.C_Inserir(pedido);

                }
                    );
            }
            finally
            {
                repositorio.FecharConexao();
            }
        }

        public List<Controle> ChecarColaborador(UInt16 setor)
        {
            try
            {
                repositorio.AbrirConexao();
                return repositorio.R_ChecarColaborador(setor);
            }
            finally
            {
                repositorio.FecharConexao();
            }
        }

        public List<Controle> ChecarCliente(UInt64 IdCliente)
        {
            try
            {
                repositorio.AbrirConexao();
                return repositorio.R_ChecarCliente(IdCliente);
            }
            finally
            {
                repositorio.FecharConexao();
            }
        }

        public void AtualizarStatus(Pedido pedido)
        {
            try
            {
                repositorio.AbrirConexao();
                repositorio.U_AtualizarStatus(pedido);
            }
            finally
            {
                repositorio.FecharConexao();
            }
        }

        private static void ValidarInserir(Pedido pedido)
        {
            if (pedido == null) throw new ValidacaoException("JSON mal formatado ou vazio.");
            if (!Array.Exists([0, 1, 2], existe => existe == pedido.Status)) throw new ValidacaoException("Status do pedido mal formatado");
        }
    }

    
}
