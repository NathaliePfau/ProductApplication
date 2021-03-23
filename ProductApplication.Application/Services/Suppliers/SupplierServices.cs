using ProductApplication.Application.Models.Address;
using ProductApplication.Application.Models.Supplieries;
using ProductApplication.Domain.AppFlowControl;
using ProductApplication.Domain.ComplexType;
using ProductApplication.Domain.Entities;
using ProductApplication.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApplication.Application.Services.Suppliers
{
    public class SupplierService : ISupplierService
    {
        private const string EXCEPTION_IT_EXISTS = "Fornecedor já cadastrado, favor cadasatrar um que não exista!";
        private const string EXEPTION_COMUNICATION_ERROR = "Ocorreu um Erro na Comunicação! ";

        private readonly ISupplierRepository _repository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _repository = supplierRepository;
        }

        public async Task<SupplierResponseModel> Create(SupplierRequestModel supplierRequestModel)
        {
            try
            {
                bool supplierChecked = await _repository.ItExists(supplierRequestModel.CNPJ);
                if (supplierChecked)
                {
                    throw new ServicesException(EXCEPTION_IT_EXISTS);
                }

                var supplier = new Supplier(
                    supplierRequestModel.CompanyName,
                    supplierRequestModel.CNPJ,
                    supplierRequestModel.Trade,
                    CreateAddress(supplierRequestModel.Address),
                    supplierRequestModel.ContactEmail,
                    supplierRequestModel.Telephone);

                supplier.Validate();
                await _repository.Create(supplier);
                var supplierResponse = CreateResponse(supplier);
                return supplierResponse;
            }
            catch (Exception ex)
            {
                throw new ServicesException(EXEPTION_COMUNICATION_ERROR, ex);
            }
        }

        public async Task<SupplierResponseModel> Update(int id, SupplierRequestModel supplierRequestModel)
        {
            try
            {
                bool supplierChecked = await _repository.OtherCNPJ(id, supplierRequestModel.CNPJ);

                if (supplierChecked)
                {
                    throw new ServicesException(EXCEPTION_IT_EXISTS);
                }

                Supplier supplier = await SupplierGet(id);

                supplier.Update(
                supplierRequestModel.CompanyName,
                supplierRequestModel.CNPJ,
                supplierRequestModel.Trade,
                CreateAddress(supplierRequestModel.Address),
                supplierRequestModel.ContactEmail,
                supplierRequestModel.Telephone);

                supplier.Validate();

                await _repository.Update(supplier);

                return CreateResponse(supplier);
            }
            catch (Exception ex)
            {
                throw new ServicesException(EXEPTION_COMUNICATION_ERROR, ex);
            }
        }

        public async Task<SupplierResponseModel> Get(int id)
        {
            try
            {
                var supplier = await SupplierGet(id);
                var supplierResponse = CreateResponse(supplier);
                return supplierResponse;
            }
            catch (Exception ex)
            {
                throw new ServicesException(EXEPTION_COMUNICATION_ERROR, ex);
            }
        }

        public async Task<IEnumerable<SupplierResponseModel>> GetAll()
        {
            try
            {
                var suppliers = await _repository.GetAll();

                return suppliers.Select(supplier => CreateResponse(supplier));
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
                var supplier = await SupplierGet(id);
                await _repository.Delete(supplier);
            }
            catch (Exception ex)
            {
                throw new ServicesException(EXEPTION_COMUNICATION_ERROR, ex);
            }
        }

        private static SupplierResponseModel CreateResponse(Supplier supplier)
        {
            return new SupplierResponseModel()
            {
                Id = supplier.Id,
                CompanyName = supplier.CompanyName,
                CNPJ = supplier.CNPJ,
                Trade = supplier.Trade,

                Address = new AddressModel()
                {
                    ZipCode = supplier.Address.ZipCode,
                    Neighborhood = supplier.Address.Neighborhood,
                    Street = supplier.Address.Street,
                    City = supplier.Address.City,
                    State = supplier.Address.State,
                    Number = supplier.Address.Number,
                    Complement = supplier.Address.Complement
                },

                ContactEmail = supplier.ContactEmail,
                Telephone = supplier.Telephone,
                Deleted = supplier.Deleted
            };
        }

        private async Task<Supplier> SupplierGet(int id)
        {
            var supplier = await _repository.Get(id);
            if (supplier == null)
            {
                throw new ServicesException("Não existe um fornecedor com esse id");
            }

            if (supplier.Deleted)
            {
                throw new ServicesException("Esse fornecedor foi inativado,favor escolher um ativo");
            }
            return supplier;
        }

        private static Address CreateAddress(AddressModel addressModel)
        {
            return new Address(
                                    addressModel.ZipCode,
                                    addressModel.Neighborhood,
                                    addressModel.Street,
                                    addressModel.City,
                                    addressModel.State,
                                    addressModel.Number,
                                    addressModel.Complement
                                );
        }
    }
}
