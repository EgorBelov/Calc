namespace CaclApi.DAL.Entities
{
    public class MealComposition
    {
        public Guid Id { get; set; }
        public int? ProductQuantity { get; set; }

        public int? ProductId { get; set; }      // внешний ключ
        public Product? Product { get; set; }    // навигационное свойство

        public int? MealId { get; set; }      // внешний ключ
        public Meal? Meal { get; set; }    // навигационное свойство
    }
}
