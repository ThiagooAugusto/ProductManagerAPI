using ProductManagerAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductManagerAPI.DTOs
{
    public class ProdutoUpdateEstoqueRequestDTO
    {
        [Required]
        [Range(0, 9999,ErrorMessage ="Insira valores entre 0 e 9999!")]
        public int Quantidade {  get; set; }
    }
}
