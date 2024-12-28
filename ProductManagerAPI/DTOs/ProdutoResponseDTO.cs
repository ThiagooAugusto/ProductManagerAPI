using ProductManagerAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductManagerAPI.DTOs
{
    public class ProdutoResponseDTO
    {
        public int Id {  get; set; }
        public string Nome { get; set; }
        public string Descricao {  get; set; }

        public decimal Preco { get; set; }

        public int CategoriaId { get; set; }
    }
}
