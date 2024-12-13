using ProductManagerAPI.Models;
using ProductManagerAPI.Services.Interfaces;

namespace ProductManagerAPI.Services
{
    public interface IProdutoService : IService<Produto>
    {
        Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id);
        Task<IEnumerable<Produto>> GetProdutosEmEstoqueAsync();
    }
}
