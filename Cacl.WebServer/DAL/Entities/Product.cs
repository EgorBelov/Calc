namespace CaclApi.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public int? ProductCategoryId { get; set; }      // внешний ключ
        public ProductCategory? ProductCategory { get; set; }    // навигационное свойство
        public List<Ingredient>? Ingredients { get; set; } = new();
    }
}
