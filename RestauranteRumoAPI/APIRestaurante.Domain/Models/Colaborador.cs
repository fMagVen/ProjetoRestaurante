namespace APIRestaurante.Domain.Models
{
    public enum EnumPermissaoUsuario
    {
        Admin = 1,
        User
    }
    public class Colaborador
    {
        public UInt32? IdColaborador { get; set; }

        public UInt16? IdAdmin { get; set; }

        public string? Nome { get; set; }

        public string? Senha { get; set; }

        public bool Descriptografado { get; set; }

        public UInt16? IdSetor { get; set; }

        public string? NomeSetor { get; set; }

        public string? StringToken { get; set; }

        public DateTime? ExpireToken { get; set; }
    }
}
