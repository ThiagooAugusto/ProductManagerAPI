namespace ProductManagerAPI.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoriaRepository CategoriaRepository { get; }
        public IProdutoRepository ProdutoRepository { get; }
        Task Commit();
    }
}
