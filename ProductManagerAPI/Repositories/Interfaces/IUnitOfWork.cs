namespace ProductManagerAPI.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoriaRepository CategoriaRepository { get; }
        public IProdutoRepository ProdutoRepository { get; }
        public TRepository GetRepository<TRepository>() where TRepository : class;  
        Task Commit();
    }
}
