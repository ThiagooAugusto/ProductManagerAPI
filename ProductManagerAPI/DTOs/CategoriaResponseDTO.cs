using ProductManagerAPI.Models;

namespace ProductManagerAPI.DTOs
{
    public class CategoriaResponseDTO
    {
        public string Nome {  get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}
