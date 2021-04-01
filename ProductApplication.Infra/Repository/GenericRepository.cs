using Microsoft.EntityFrameworkCore;
using ProductApplication.Domain.Entities;
using ProductApplication.Domain.Interfaces;
using ProductApplication.Infra.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApplication.Infra.Repository
{
    public class GenericRepository<TEntity>
        : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MainContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(MainContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task<TEntity> Get(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task Create(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Task.FromResult(_dbSet.AsNoTracking());
        }

        protected IQueryable<TEntity> Query() => _dbSet.AsNoTracking().Where(w => !w.Deleted);

        public async Task Delete(TEntity entity)
        {
            if (entity != null)
            {
                entity.Delete();
                await Update(entity);
            }
        }

        public async Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
