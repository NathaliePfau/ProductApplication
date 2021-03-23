using ProductApplication.Application.Models.Address;

namespace ProductApplication.Application.Models.Supplieries
{
    public abstract class SupplierBaseModel
    {
        public string CompanyName { get; set; }
        public string CNPJ { get; set; }
        public string Trade { get; set; }
        public AddressModel Address { get; set; }
        public string ContactEmail { get; set; }
        public string Telephone { get; set; }
    }
}
