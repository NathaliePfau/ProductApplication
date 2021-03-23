using ProductApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductApplication.Domain.Interfaces
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        Task<bool> ItExists(string cnpj);
        Task<bool> OtherCNPJ(int id, string cnpj);
    }
}
