using ProductApplication.Application.Models.Categories;
using System.Collections.Generic;
using System.Text;

namespace ProductApplication.Application.Export
{
    public class CategoryExport
    {
        public string Export(List<CategoryResponseModel> categoryResponse)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Category Name;Supplier Trade");

            foreach (var item in categoryResponse)
            {
                builder.AppendLine($"{ item.Name};{item.TradeSupplier}");
            }
            return builder.ToString();
        }
    }
}
