namespace CaclApi.DAL.Entities
{
    public class MealCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Meal> Meals { get; set; }
    }
}
