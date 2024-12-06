using ProductManagerAPI.Context;
using ProductManagerAPI.Models;
using ProductManagerAPI.Repositories.Interfaces;
using System.Linq.Expressions;

namespace ProductManagerAPI.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Produto>> GetProdutosEmEstoqueAsync()
        {
            var produtos = await GetAllAsync();
            var produtosEmEstoque = produtos.Where(p => p.Status == Status.EmEstoque);
            return produtosEmEstoque;
        }

        public async Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id)
        {
            var produtos = await GetAllAsync();
            var produtosCategoria = produtos.Where(c => c.CategoriaId == id);
            return produtosCategoria;
        }
    }
}
