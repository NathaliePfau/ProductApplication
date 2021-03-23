using Microsoft.AspNetCore.Http;
using ProductApplication.Application.Export;
using ProductApplication.Application.Import;
using ProductApplication.Application.Models.Categories;
using ProductApplication.Domain.AppFlowControl;
using ProductApplication.Domain.Entities;
using ProductApplication.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApplication.Application.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private const string EXCEPTION_IT_EXISTS = "Categoria já cadastrado, favor cadasatrar uma que não exista!";
        private const string EXEPTION_COMUNICATION_ERROR = "Ocorreu um Erro na Comunicação! ";

        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _repository = categoryRepository;
        }

        public async Task<CategoryResponseModel> Create(CategoryRequestModel categoryRequestModel)
        {
            try
            {
                bool categoryChecked = await _repository.ItExists(categoryRequestModel.Name);
                if (categoryChecked)
                {
                    throw new ServicesException(EXCEPTION_IT_EXISTS);
                }

                var category = new Category(categoryRequestModel.Name, categoryRequestModel.IdSupplier);

                category.Validate();
                await _repository.Create(category);
                return CreateResponse(category);
            }
            catch (Exception ex)
            {
                throw new ServicesException(EXEPTION_COMUNICATION_ERROR, ex);
            }
        }

        public async Task<CategoryResponseModel> Get(int id)
        {

            try
            {
                var category = await CategoryGet(id);
                var categoryResponse = CreateResponse(category);
                return categoryResponse;
            }
            catch (Exception ex)
            {
                throw new ServicesException(EXEPTION_COMUNICATION_ERROR, ex);
            }
        }

        public async Task<IEnumerable<CategoryResponseModel>> GetAll()
        {
            try
            {
                var categories = await _repository.GetAll();

                return categories.Select(category => CreateResponse(category));
            }
            catch (Exception ex)
            {
                throw new ServicesException(EXEPTION_COMUNICATION_ERROR, ex);
            }
        }

        public async Task<CategoryResponseModel> Update(int id, CategoryRequestModel categoryRequestModel)
        {
            try
            {
                bool categoryChecked = await _repository.ItExists(categoryRequestModel.Name);
                if (categoryChecked)
                {
                    throw new ServicesException(EXCEPTION_IT_EXISTS);
                }

                Category category = await CategoryGet(id);

                category.Update(
                categoryRequestModel.Name,
                categoryRequestModel.IdSupplier);

                category.Validate();

                await _repository.Update(category);

                return CreateResponse(category);
            }
            catch (Exception ex)
            {
                throw new ServicesException(EXEPTION_COMUNICATION_ERROR, ex);
            }
        }
        public async Task Delete(int id)
        {
            try
            {
                var category = await CategoryGet(id);
                await _repository.Delete(category);
            }
            catch (Exception ex)
            {
                throw new ServicesException(EXEPTION_COMUNICATION_ERROR, ex);
            }
        }

        public async Task<byte[]> Export()
        {
            var categoryResponse = new List<CategoryResponseModel>();

            foreach (var item in await _repository.GetAllSupplier())
            {
                categoryResponse.Add(CreateResponse(item));
            }

            var categoriaExportada = new CategoryExport();
            var builder = categoriaExportada.Export(categoryResponse);
            return Encoding.Unicode.GetBytes(builder);
        }

        public async Task<IEnumerable<CategoryResponseModel>> Import(IFormFile filePath)
        {
            var categoryList = new List<CategoryResponseModel>();
            foreach (var category in CategoryImport.Import(filePath))
            {
                var categoryDatas = category.Split(new[] { ',' });
                if (categoryDatas[0] != null && categoryDatas[1] != null)
                {
                    bool categoryChecked = await _repository.ItExists(categoryDatas[0]);
                    if (categoryChecked)
                    {
                        throw new ServicesException(EXCEPTION_IT_EXISTS);
                    }
                    var newCategory = new Category(categoryDatas[0], int.Parse(categoryDatas[1]));
                    newCategory.Validate();
                    await _repository.Create(newCategory);
                    categoryList.Add(CreateResponse(newCategory));
                }
            }
            return categoryList;
        }

        private static CategoryResponseModel CreateResponse(Category category)
        {
            return new CategoryResponseModel(category);
        }

        private async Task<Category> CategoryGet(int id)
        {
            var category = await _repository.Get(id);
            if (category == null)
            {
                throw new ServicesException("Não existe uma categoria com esse id");
            }

            if (category.Deleted)
            {
                throw new ServicesException("Essa cateogria foi inativada,favor escolher uma ativa");
            }
            return category;
        }
    }
}
