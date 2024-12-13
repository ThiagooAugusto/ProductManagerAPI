using ProductManagerAPI.Models;
using ProductManagerAPI.Repositories.Interfaces;
using ProductManagerAPI.Services.Interfaces;

namespace ProductManagerAPI.Services
{
    public class CategoriaService : Service<Categoria>, ICategoriaService
    {
        public CategoriaService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
