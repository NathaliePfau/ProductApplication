using Microsoft.EntityFrameworkCore;
using ProductApplication.Domain.Entities;
using ProductApplication.Domain.Interfaces;
using ProductApplication.Infra.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApplication.Infra.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(MainContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> ItExists(string name)
        {
            return Query().AnyAsync(x => x.Name == name);
        }

        public async Task<List<Category>> GetAllSupplier()
        {
            return await Query().Include(x => x.SupplierCategory).ToListAsync();
        }
    }
}
