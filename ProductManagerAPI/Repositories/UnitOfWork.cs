using ProductManagerAPI.Context;
using ProductManagerAPI.Models;
using ProductManagerAPI.Repositories.Interfaces;

namespace ProductManagerAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICategoriaRepository _categoriaRepo;
        private IProdutoRepository _produtoRepo;
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICategoriaRepository CategoriaRepository
        {
            get => _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
        }
        public IProdutoRepository ProdutoRepository 
        { 
            get => _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context); 
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
        public TRepository GetRepository<TRepository>() where TRepository : class
        {
            if (typeof(TRepository) == typeof(IBaseRepository<Produto>))
                return ProdutoRepository as TRepository;

            if (typeof(TRepository) == typeof(IBaseRepository<Categoria>))
                return CategoriaRepository as TRepository;

            throw new InvalidOperationException("Repositório não suportado.");
        }
    }
}
