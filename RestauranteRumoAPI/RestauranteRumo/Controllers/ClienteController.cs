using APIRestaurante.Domain.Exceptions;
using APIRestaurante.Domain.Models;
using APIRestaurante.Services;
using Microsoft.AspNetCore.Mvc;

namespace RestauranteRumo.Controllers
{
    [ApiController]
    public class ClienteController(ClienteService service) : ControllerBase
    {
        [HttpPost("cliente")]
        public IActionResult Criar(Cliente cliente)
        {
            try
            {
                long id = service.Criar(cliente);
                return StatusCode(201, id);
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
