namespace ProductApplication.Application.Models.Categories
{
    public abstract class CategoryBaseModel
    {
        public string Name { get; set; }
        public int IdSupplier { get; set; }
        public string TradeSupplier { get; set; }
    }
}
