namespace ProductApplication.Application.Models.Categories
{
    public class CategoryResponseModel : CategoryBaseModel
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }

        public CategoryResponseModel(Category item)
        {
            Id = item.Id;
            Name = item.Name;
            IdSupplier = item.IdSupplier;
            Deleted = item.Deleted;
            TradeSupplier = item.SupplierCategory?.Trade;
        }
    }
}
