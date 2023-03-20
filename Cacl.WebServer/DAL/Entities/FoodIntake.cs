namespace CaclApi.DAL.Entities
{
    public class FoodIntake
    {
        public int Id { get; set; }
        public int? FoodIntakeTypeId { get; set; }      // внешний ключ
        public FoodIntakeType? FoodIntakeType { get; set; }    // навигационное свойство
        public DateTime? Date { get; set; }
        public List<Meal> Meals { get; set; } = new();
        public string? UserId { get; set; }      // внешний ключ
        public User? User { get; set; }    // навигационное свойство
    }
}
