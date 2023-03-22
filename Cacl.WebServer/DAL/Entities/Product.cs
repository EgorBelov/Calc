namespace CaclApi.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Calories { get; set; }
        public int? ProductCategoryId { get; set; }      
        public ProductCategory? ProductCategory { get; set; }  
        public List<Ingredient>? Ingredients { get; set; } = new();
    }
}
