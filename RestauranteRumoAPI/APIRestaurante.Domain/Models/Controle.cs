namespace APIRestaurante.Domain.Models
{
    public class Controle
    {
        public required UInt64 IdPedido { get; set; }

        public required UInt32 IdItem { get; set; }

        public required string NomeItem { get; set; }

        public UInt16 Categoria { get; set; }

        public UInt16 Status { get; set; }
        
        public UInt32 Quantidade { get; set; }

        public string? NomeCliente { get; set; }

        public UInt16? Mesa { get; set; }
    }
}
