using ProductManagerAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductManagerAPI.DTOs
{
    public class ProdutoUpdateEstoqueRequestDTO
    {
        [Required]
        [Range(0, 9999,ErrorMessage ="Insera valores entre 0 e 9999!")]
        public int Quantidade {  get; set; }

        [Required]
        [Range(0,1,ErrorMessage ="Insira valor 0(Indisponivel) ou 1 (Em estoque) !")]
        public Status Status { get; set; }
    }
}
