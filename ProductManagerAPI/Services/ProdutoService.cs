using ProductManagerAPI.Context;
using ProductManagerAPI.Models;
using ProductManagerAPI.Repositories.Interfaces;
using ProductManagerAPI.Services.Interfaces;

namespace ProductManagerAPI.Services
{
    public class ProdutoService : Service<Produto>, IProdutoService
    {
        public ProdutoService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IEnumerable<Produto>> GetProdutosEmEstoqueAsync()
        {
            return await _unitOfWork.ProdutoRepository.GetProdutosEmEstoqueAsync();
        }

        public async Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id)
        {
            return await _unitOfWork.ProdutoRepository.GetProdutosPorCategoriaAsync(id);
        }
    }
}
