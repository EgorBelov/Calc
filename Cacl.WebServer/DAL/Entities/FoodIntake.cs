namespace CaclApi.DAL.Entities
{
    public class FoodIntake
    {
        public Guid Id { get; set; }
        public Guid? FoodIntakeTypeId { get; set; }      // внешний ключ
        public FoodIntakeType? FoodIntakeType { get; set; }    // навигационное свойство
        public DateTime? Date { get; set; }
        public Guid? MealId { get; set; }      // внешний ключ
        public Meal? Meal { get; set; }    // навигационное свойство
        public Guid? UserId { get; set; }      // внешний ключ
        public User? User { get; set; }    // навигационное свойство
    }
}
