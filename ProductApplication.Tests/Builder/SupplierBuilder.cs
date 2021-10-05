using ProductApplication.Domain.ComplexType;
using ProductApplication.Domain.Entities;

namespace ProductApplication.Tests.Builder
{
    public class SupplierBuilder
    {
        private string _companyName;
        private string _cnpj;
        private string _trade;
        private Address _address;
        private string _contactEmail;
        private string _telephone;
        private int _ID;

        public Supplier Build()
        {
            return new SupplierTest(_companyName, _cnpj, _trade, _address, _contactEmail, _telephone, _ID)
            {
            };
        }

        public SupplierBuilder UseCompanyName(string companyName)
        {
            _companyName = companyName;
            return this;
        }

        public SupplierBuilder UseCnpj(string cnpj)
        {
            _cnpj = cnpj;
            return this;
        }

        public SupplierBuilder UseTrade(string trade)
        {
            _trade = trade;
            return this;
        }

        public SupplierBuilder UseAddress(Address address)
        {
            _address = address;
            return this;
        }

        public SupplierBuilder UseContactEmail(string contactEmail)
        {
            _contactEmail = contactEmail;
            return this;
        }

        public SupplierBuilder UseTelephone(string telephone)
        {
            _telephone = telephone;
            return this;
        }

        public SupplierBuilder UseId(int id)
        {
            _ID = id;
            return this;
        }

        internal class SupplierTest : Supplier
        {
            public SupplierTest(string companyName, string cNPJ, string trade, Address address, string contactEmail, string telephone, int iDTest) : base(companyName, cNPJ, trade, address, contactEmail, telephone)
            {
                Id = iDTest;
            }
        }
    }
}
