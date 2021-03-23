using ProductApplication.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApplication.Domain.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<bool> ItExists(string name);
        Task<List<Category>> GetAllSupplier();

    }
}
