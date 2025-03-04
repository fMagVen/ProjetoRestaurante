using APIRestaurante.Domain.Exceptions;
using APIRestaurante.Domain.Models;
using APIRestaurante.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestauranteRumo.Controllers
{
    [ApiController]
    public class CardapioController(CardapioService service) : ControllerBase
    {
        [Authorize(Roles = "1")]
        [HttpPost("cardapio")]
        public IActionResult Criar(ItemCardapio item) {
            try
            {
                service.Criar(item);
                return StatusCode(201);
            }
            catch (ValidacaoException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex) {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("cardapio")]
        public IActionResult Listar_n([FromQuery] string? offset)
        {
            return StatusCode(200, service.Listar_n(offset));
        }

        [HttpGet("cardapio/{idPrato}")]
        public IActionResult Listar_um([FromRoute] string idItem)
        {
            return StatusCode(200);
        }

        [Authorize(Roles = "1")]
        [HttpPut("cardapio")]
        public IActionResult Alterar(ItemCardapio item)
        {
            service.Atualizar(item);
            return StatusCode(200);
        }

        [Authorize(Roles = "1")]
        [HttpDelete("cardapio")]
        public IActionResult Deletar(string idItem)
        {
            service.Deletar(idItem);
            return StatusCode(200);
        }
    }
}
