using ProductManagerAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProductManagerAPI.Vallidation;

namespace ProductManagerAPI.DTOs
{
    public class ProdutoCreateDTO
    {
       
        [Required(ErrorMessage = "Campo Nome obrigatório!")]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(200,ErrorMessage ="Descricao deve ter no maximo 200 caracteres!")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Range(0,9999,ErrorMessage ="Insira valores entre 0 e 9999!")]
        public decimal Preco { get; set; }

        [Required]
        public int CategoriaId {  get; set; }

    }
}
