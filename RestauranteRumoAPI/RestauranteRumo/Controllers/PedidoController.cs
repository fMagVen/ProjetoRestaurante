using APIRestaurante.Domain.Exceptions;
using APIRestaurante.Domain.Models;
using APIRestaurante.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestauranteRumo.Controllers
{
    [ApiController]
    public class PedidoController(PedidoService service) : ControllerBase
    {
        [HttpPost("pedido")]
        public IActionResult Criar(List<Pedido> pedidos)
        {
            try
            {
                service.Criar(pedidos);
                return StatusCode(201);
            }
            catch (ValidacaoException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
        [Authorize(Roles = "2")]
        [HttpGet("pedido/{setor}")]
        public IActionResult Listar([FromRoute] UInt16 setor)
        {
            try
            {
                return StatusCode(202, service.ChecarColaborador(setor));
            }
            catch (ValidacaoException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("pedido/cliente/{idCliente}")]
        public IActionResult Listar([FromRoute] UInt64 idCliente)
        {
            try
            {
                return StatusCode(202, service.ChecarCliente(idCliente));
            }
            catch (ValidacaoException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [Authorize(Roles = "2")]
        [HttpPost("pedido/status")]
        public IActionResult AtualizarStatus(Pedido pedido)
        {
            try
            {
                service.AtualizarStatus(pedido);
                return StatusCode(200);
            }
            catch (ValidacaoException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
