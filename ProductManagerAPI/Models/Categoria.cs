using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ProductManagerAPI.Models
{
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public ICollection<Produto> Produtos { get; set; }
    }
}
