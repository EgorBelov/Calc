namespace CaclApi.DAL.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public int? ProductQuantity { get; set; }
        public string Description { get; set; }
        public int? ProductId { get; set; }      // внешний ключ
        public Product? Product { get; set; }    // навигационное свойство

        public List<Meal> Meals { get; set; } = new();
       
    }
}
