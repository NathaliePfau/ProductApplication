using Microsoft.EntityFrameworkCore;
using ProductApplication.Domain.Entities;
using ProductApplication.Domain.Interfaces;
using ProductApplication.Infra.Context;
using System.Threading.Tasks;

namespace ProductApplication.Infra.Repository
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(MainContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Supplier> Get(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(e => e.Address)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<bool> ItExists(string cnpj)
        {
            return Query().AnyAsync(x => x.CNPJ == cnpj);
        }

        public async Task<bool> OtherCNPJ(int id, string cnpj)
        {
            return await Query().AnyAsync(x => x.CNPJ == cnpj && x.Id != id);
        }
    }
}
