namespace APIRestaurante.Domain.Models
{
    public class Cliente
    {
        public uint? IdCliente {  get; set; }

        public required string Nome { get; set; }

        public UInt32? Mesa { get; set; }

        public required DateTime Data { get; set; }

        public required bool Fechado { get; set; }
    }
}