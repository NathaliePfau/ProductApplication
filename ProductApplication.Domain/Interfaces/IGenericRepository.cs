using ProductApplication.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApplication.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task Create(TEntity entity);
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
