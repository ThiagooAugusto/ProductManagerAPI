using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagerAPI.Models
{
    public class Produto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(200)]
        public string Descricao {  get; set; }
        public Status Status { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco {  get; set; }

        [Range(0, 9999)]
        public int Quantidade {  get; set; }

        public Categoria Categoria { get; set; }

        public int CategoriaId {  get; set; }
    }
    public enum Status
    {
        Indisponivel = 0,
        EmEstoque = 1
    }
}
