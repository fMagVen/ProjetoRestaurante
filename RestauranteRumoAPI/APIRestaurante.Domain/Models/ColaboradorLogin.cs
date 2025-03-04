namespace APIRestaurante.Domain.Models
{
    public class ColaboradorLogin
    {
        public UInt32 IdColaborador { get; set; }

        public required string Senha { get; set; }
    }
}
