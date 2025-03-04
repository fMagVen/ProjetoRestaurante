using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APIRestaurante.Domain.Exceptions;
using APIRestaurante.Domain.Models;
using APIRestaurante.Repositories.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace APIRestaurante.Services
{
    public class ColaboradorService
    {
        private readonly ColaboradorRepository _repositorio;
        private readonly IConfiguration _config;

        public ColaboradorService(ColaboradorRepository repositorio, IConfiguration config)
        {
            _repositorio = repositorio;
            _config = config;
        }

        public long C_Cadastrar(Colaborador colaborador)
        {
            try
            {
                ValidarCadastro(colaborador);
                _repositorio.AbrirConexao();
                return _repositorio.C_Cadastrar(colaborador);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public Colaborador R_Logar(Colaborador colaborador)
        {
            try
            {
                ValidarLogin(colaborador);
                Colaborador retornoCompleto = _repositorio.R_Logar(colaborador);
                Colaborador tokenGerado = GerarToken(_config, retornoCompleto.NomeSetor);
                retornoCompleto.StringToken = tokenGerado.StringToken;
                retornoCompleto.ExpireToken = tokenGerado.ExpireToken;
                return retornoCompleto;
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public Colaborador? R_Consultar(Colaborador colaborador)
        {
            try
            {
                ValidarConsulta(colaborador);
                _repositorio.AbrirConexao();
                return _repositorio.R_Consultar(colaborador);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public List<Setor> R_LerSetores()
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.R_LerSetores();
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public bool U_Atualizar(Colaborador colaborador)
        {
            try
            {
                ValidarLogin(colaborador);
                _repositorio.AbrirConexao();
                return _repositorio.U_Atualizar(colaborador);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public bool D_Desligar(Colaborador colaborador)
        {
            try
            {
                ValidarConsulta(colaborador);
                _repositorio.AbrirConexao();
                return _repositorio.D_Desligar(colaborador.IdColaborador);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        private static void ValidarCadastro(Colaborador colaborador)
        {
            if (colaborador == null) throw new ValidacaoException("JSON mal formatado ou vazio.");
            if (string.IsNullOrEmpty(colaborador.Nome)) throw new ValidacaoException("O Colaborador deve cadastrar o nome.");
            if (string.IsNullOrEmpty(colaborador.Senha)) throw new ValidacaoException("O Colaborador deve cadastrar uma senha.");
        }

        private void ValidarLogin(Colaborador colaborador)
        {
            if (colaborador == null) throw new ValidacaoException("JSON mal formatado ou vazio.");
            if (!colaborador.IdColaborador.HasValue) throw new ValidacaoException("O Colaborador deve inserir o ID.");
            if (string.IsNullOrEmpty(colaborador.Senha)) throw new ValidacaoException("O Colaborador deve inserir a senha.");
            if (colaborador.Descriptografado) colaborador.Senha = CriptografarSHA512(colaborador.Senha);
            _repositorio.AbrirConexao();
            if (!_repositorio.R_ValidarLogin(colaborador)) throw new ValidacaoException("Colaborador ou senha inválidos.");
        }

        private void ValidarConsulta(Colaborador colaborador)
        {
            if (colaborador == null) throw new ValidacaoException("JSON mal formatado ou vazio.");
            if (!colaborador.IdAdmin.HasValue) throw new ValidacaoException("O Colaborador deve fornecer o ID para validar privilégios.");
            UInt16? encontrado = _repositorio.R_ValidarConsulta(colaborador.IdAdmin);
            if (encontrado != 0) throw new ValidacaoException("Este Colaborador não tem permissão para consultar ou desligar outros colaboradores.");
        }

        private static string CriptografarSHA512(string senha)
        {
            var bytes = Encoding.UTF8.GetBytes(senha);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString().ToLower();
            }
        }

        private static Colaborador GerarToken(IConfiguration _config, string? setor)
        {
            var senhaJwt = Encoding.ASCII.GetBytes(_config["SenhaJwt"] as string ?? string.Empty);
            UInt16 codSetor;
            if (setor == "Admin")
                codSetor = 1;
            else
                codSetor = 2;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([new Claim("IdColaborador", Guid.NewGuid().ToString()), new Claim(ClaimTypes.Role, codSetor.ToString())]),
                Expires = DateTime.UtcNow.AddHours(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(senhaJwt), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return new Colaborador
            {
                StringToken = stringToken,
                ExpireToken = tokenDescriptor.Expires
            };
        }
    }
}
