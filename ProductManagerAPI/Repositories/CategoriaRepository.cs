using ProductManagerAPI.Context;
using ProductManagerAPI.Models;
using ProductManagerAPI.Repositories.Interfaces;

namespace ProductManagerAPI.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {
        }
    }
}
