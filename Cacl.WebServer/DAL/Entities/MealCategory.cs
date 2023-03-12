namespace CaclApi.DAL.Entities
{
    public class MealCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Meal> Meals { get; set; }
    }
}
