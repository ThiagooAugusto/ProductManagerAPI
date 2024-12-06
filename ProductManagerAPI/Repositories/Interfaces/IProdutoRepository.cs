using ProductManagerAPI.Models;

namespace ProductManagerAPI.Repositories.Interfaces
{
    public interface IProdutoRepository:IBaseRepository<Produto>
    {
        Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id);
        Task<IEnumerable<Produto>> GetProdutosEmEstoqueAsync();
    }
}
