using APIRestaurante.Domain.Exceptions;
using APIRestaurante.Domain.Models;
using APIRestaurante.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestauranteRumo.Controllers
{
    [ApiController]
    public class ColaboradorController(ColaboradorService service) : ControllerBase
    {
        [Authorize(Roles = "1")]
        [HttpPost("colaborador/cadastro")]
        public IActionResult Cadastrar(Colaborador colaborador)
        {
            try
            {
                return StatusCode(201, service.C_Cadastrar(colaborador));
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

        [HttpPost("colaborador/login")]
        public IActionResult Logar(Colaborador colaborador)
        {
            try
            {
                return StatusCode(200, service.R_Logar(colaborador));
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

        [Authorize(Roles = "1")]
        [HttpGet("colaborador/{idColaborador}")]
        public IActionResult Consultar(Colaborador colaborador, [FromRoute] UInt32 idColaborador)
        {
            try
            {
                return StatusCode(200, service.R_Consultar(colaborador));
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

        [Authorize(Roles = "1")]
        [HttpGet("setores")]
        public IActionResult ConsultarSetores()
        {
            try
            {
                return StatusCode(200, service.R_LerSetores());
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

        [Authorize(Roles = "1")]
        [HttpPut("colaborador")]
        public IActionResult Atualizar(Colaborador colaborador)
        {
            try
            {
                service.U_Atualizar(colaborador);
                return StatusCode(202);
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

        [Authorize(Roles = "1")]
        [HttpDelete("colaborador")]
        public IActionResult Desligar(Colaborador colaborador)
        {
            try
            {
                service.D_Desligar(colaborador);
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
