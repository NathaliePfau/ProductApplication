using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApplication.Application.Models.Categories;
using ProductApplication.Application.Services.Categories;
using ProductApplication.Domain.AppFlowControl;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApplication.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private const string COMMUNICATION_ERROR = "Erro na comunicação";
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryResponseModel>> Get()
        {
            try
            {
                return await _categoryService.GetAll();
            }
            catch (Exception ex)
            {
                throw new ServicesException(COMMUNICATION_ERROR, ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CategoryResponseModel>> Get([FromRoute] int id)
        {
            try
            {
                return await _categoryService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServicesException(COMMUNICATION_ERROR, ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryRequestModel model)
        {
            try
            {
                var category = await _categoryService.Create(model);
                return CreatedAtRoute("", category);
            }
            catch (Exception ex)
            {
                throw new ServicesException(COMMUNICATION_ERROR, ex);
            }
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] CategoryRequestModel model)
        {
            try
            {
                await _categoryService.Update(id, model);
                return Ok("Categoria Atualizada");
            }
            catch (Exception ex)
            {
                throw new ServicesException(COMMUNICATION_ERROR, ex);
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _categoryService.Delete(id);
                return Ok("Categoria Deletada");
            }
            catch (Exception ex)
            {
                throw new ServicesException(COMMUNICATION_ERROR, ex);
            }
        }

        [Route("csv")]
        [HttpGet]
        public async Task<IActionResult> Export()
        {
            try
            {
                return File(await _categoryService.Export(), "text/csv", "category.csv");

            }
            catch (Exception ex)
            {
                throw new ServicesException(COMMUNICATION_ERROR, ex);
            }
        }

        [Route("import")]
        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            try
            {
                await _categoryService.Import(file);
                return Ok("Categoria Importada");
            }
            catch (Exception ex)
            {
                throw new ServicesException(COMMUNICATION_ERROR, ex);
            }
        }
    }
}
