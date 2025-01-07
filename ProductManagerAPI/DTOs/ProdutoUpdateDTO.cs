using System.ComponentModel.DataAnnotations;

namespace ProductManagerAPI.DTOs
{
    public class ProdutoUpdateDTO
    {
        [Required(ErrorMessage ="Campo Id obrigatório!")]
        public int Id {  get; set; }

        [Required(ErrorMessage = "Campo Nome obrigatório!")]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(200, ErrorMessage = "Descricao deve ter no maximo 200 caracteres!")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Range(0, 9999, ErrorMessage = "Insira valores entre 0 e 9999!")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage ="Campo obrigatório!")]
        public int CategoriaId { get; set; }
    }
}
