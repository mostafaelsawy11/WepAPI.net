namespace ItIAssIgnment.DTO
{
    public class CategoryWithProducts
    {
        public string Name { get; set; }
        public List<ProductName> products { get; set; } = new List<ProductName>();
    }
    public class ProductName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
