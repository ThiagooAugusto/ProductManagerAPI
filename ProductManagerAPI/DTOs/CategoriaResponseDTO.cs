using ProductManagerAPI.Models;

namespace ProductManagerAPI.DTOs
{
    public class CategoriaResponseDTO
    {
        public int Id { get; set; }
        public string Nome {  get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}
