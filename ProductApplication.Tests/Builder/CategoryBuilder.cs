using ProductApplication.Domain.Entities;

namespace ProductApplication.Tests.Builder
{
    public class CategoryBuilder
    {
        private string _name;
        private int _idSupplier;
        private int _id;
        private Supplier _supplier;
        public Category Build()
        {
            return new CategoryTest(_name, _idSupplier, _id, _supplier) { };
        }

        public CategoryBuilder UseName(string name)
        {
            _name = name;
            return this;
        }

        public CategoryBuilder UseIdSupplier(int idSupplier)
        {
            _idSupplier = idSupplier;
            return this;
        }

        public CategoryBuilder UseId(int id)
        {
            _id = id;
            return this;
        }
        public CategoryBuilder UseSupplier(Supplier supplierCategory)
        {
            _supplier = supplierCategory;
            return this;
        }

        internal class CategoryTest : Category
        {
            public CategoryTest(string name, int idSupplier, int id, Supplier supplierCategory) : base(name, idSupplier)
            {
                Id = id;
                SupplierCategory = supplierCategory;
            }
        }
    }
}
