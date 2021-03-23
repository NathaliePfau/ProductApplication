using FluentValidation;
using ProductApplication.Domain.ComplexType;
using ProductApplication.Domain.Validations;
using System.Collections.Generic;

namespace ProductApplication.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string CompanyName { get; protected set; }
        public string CNPJ { get; protected set; }
        public string Trade { get; protected set; }
        public Address Address { get; protected set; }
        public string ContactEmail { get; protected set; }
        public string Telephone { get; protected set; }
        public List<Category> Categories { get; protected set; }

        protected Supplier() { }

        public Supplier(string companyName, string cNPJ, string trade, Address address, string contactEmail, string telephone)
        {
            CompanyName = companyName;
            CNPJ = cNPJ;
            Trade = trade;
            Address = address;
            ContactEmail = contactEmail;
            Telephone = telephone;
        }

        public void Update(string companyName, string cNPJ, string trade, Address address, string contactEmail, string telephone)
        {
            CompanyName = companyName;
            CNPJ = cNPJ;
            Trade = trade;
            Address = address;
            ContactEmail = contactEmail;
            Telephone = telephone;
        }

        public void Validate()
        {
            var validate = new SupplierValidations();
            validate.ValidateAndThrow(this);
        }
    }
}
