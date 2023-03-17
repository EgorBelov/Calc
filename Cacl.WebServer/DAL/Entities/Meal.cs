namespace CaclApi.DAL.Entities
{
    public class Meal
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? MealCategoryId { get; set; }      // внешний ключ
        public MealCategory? MealCategory { get; set; }    // навигационное свойство
        public List<Ingredient> Ingredients { get; set; } = new ();
        public List<FoodIntake> FoodIntakes { get; set; } = new ();
    }
}
