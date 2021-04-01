using Microsoft.AspNetCore.Mvc;
using ProductApplication.Application.Models.Supplieries;
using ProductApplication.Application.Services.Suppliers;
using ProductApplication.Domain.AppFlowControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApplication.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private const string COMMUNICATION_ERROR = "Erro na comunicação";
        private readonly ISupplierService _fornecedorService;

        public SupplierController(ISupplierService fornecedorService)
        {
            _fornecedorService = fornecedorService;
        }

        [HttpGet]
        public async Task<IEnumerable<SupplierResponseModel>> Get()
        {
            try
            {
                return await _fornecedorService.GetAll();
            }
            catch (Exception ex)
            {
                throw new ServicesException(COMMUNICATION_ERROR, ex);
            }
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SupplierResponseModel>> Get([FromRoute] int id)
        {
            try
            {
                return await _fornecedorService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServicesException(COMMUNICATION_ERROR, ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SupplierRequestModel model)
        {
            try
            {
                var supplier = await _fornecedorService.Create(model);
                return CreatedAtRoute("", supplier);
            }
            catch (Exception ex)
            {
                throw new ServicesException(COMMUNICATION_ERROR, ex);
            }
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] SupplierRequestModel model)
        {
            try
            {
                await _fornecedorService.Update(id, model);
                return Ok("Fornecedor Atualizado");
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
                await _fornecedorService.Delete(id);
                return Ok("Fornecedor Deletado");
            }
            catch (Exception ex)
            {
                throw new ServicesException(COMMUNICATION_ERROR, ex);
            }
        }
    }
}
