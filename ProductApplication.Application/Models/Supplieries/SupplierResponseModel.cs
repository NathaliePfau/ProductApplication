using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApplication.Application.Models.Supplieries
{
    public class SupplierResponseModel : SupplierBaseModel
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
}
