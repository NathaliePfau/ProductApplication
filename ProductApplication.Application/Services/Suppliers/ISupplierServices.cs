using ProductApplication.Application.Models.Supplieries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApplication.Application.Services.Suppliers
{
    public interface ISupplierService
    {
        Task<SupplierResponseModel> Create(SupplierRequestModel supplierRequestModel);
        Task<SupplierResponseModel> Get(int id);
        Task<IEnumerable<SupplierResponseModel>> GetAll();
        Task<SupplierResponseModel> Update(int id, SupplierRequestModel supplierRequestModel);
        Task Delete(int id);
    }
}
