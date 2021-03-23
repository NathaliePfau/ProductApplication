using Microsoft.AspNetCore.Http;
using ProductApplication.Application.Models.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApplication.Application.Services.Categories
{
    public interface ICategoryService
    {
        Task<CategoryResponseModel> Create(CategoryRequestModel categoryRequestModel);
        Task<CategoryResponseModel> Get(int id);
        Task<IEnumerable<CategoryResponseModel>> GetAll();
        Task<CategoryResponseModel> Update(int id, CategoryRequestModel categoryRequestModel);
        Task Delete(int id);
        Task<byte[]> Export();
        Task<IEnumerable<CategoryResponseModel>> Import(IFormFile filePath);
    }
}
