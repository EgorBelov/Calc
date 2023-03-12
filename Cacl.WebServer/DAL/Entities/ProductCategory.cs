namespace CaclApi.DAL.Entities
{
    public class ProductCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
