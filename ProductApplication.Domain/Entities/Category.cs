using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApplication.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; protected set; }
        public int IdSupplier { get; protected set; }
        public Supplier SupplierCategory { get; protected set; }

        protected Category() { }

        public Category(string name, int idSupplier)
        {
            Name = name;
            IdSupplier = idSupplier;
        }
        public void Update(string name, int idSupplier)
        {
            Name = name;
            IdSupplier = idSupplier;
        }


        public void Validate()
        {
            var validate = new CategoryValidations();
            validate.ValidateAndThrow(this);
        }
    }
}
