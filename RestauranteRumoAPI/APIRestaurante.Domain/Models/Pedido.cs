namespace APIRestaurante.Domain.Models
{
    public class Pedido
    {
        public uint? IdPedido { get; set; }

        public required UInt16 IdItem { get; set; }

        public UInt32? IdCliente { get; set; }

        public UInt16? Quantidade { get; set; }

        public UInt16 Status { get; set; }
    }
}
