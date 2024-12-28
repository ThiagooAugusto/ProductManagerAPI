using ProductManagerAPI.Models;

namespace ProductManagerAPI.DTOs
{
    public class ProdutoUpdateEstoqueResponseDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public Status Status { get; set; }

        public decimal Preco { get; set; }

        public int Quantidade { get; set; }

        public int CategoriaId { get; set; }
    }
}
