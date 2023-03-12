namespace CaclApi.DAL.Entities
{
    public class Meal
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public int? CaloricValue { get; set; }
        public Guid? MealCategoryId { get; set; }      // внешний ключ
        public MealCategory? MealCategory { get; set; }    // навигационное свойство
        public List<MealComposition>? MealCompositions { get; set; } = new();
        public List<FoodIntake>? FoodIntakes { get; set; } = new();
    }
}
