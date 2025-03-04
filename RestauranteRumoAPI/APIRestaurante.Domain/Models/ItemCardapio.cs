namespace APIRestaurante.Domain.Models
{
    public class ItemCardapio
    {
        public UInt32 IdItem { get; set; }

        public required string Nome { get; set; }

        public UInt16 Categoria { get; set; }

        public required string IngredientesDesc { get; set; }

        public decimal Valor { get; set; }
    }
}
