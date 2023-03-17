namespace CaclApi.DAL.Entities
{
    public class FoodIntakeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<FoodIntake> FoodIntakes { get; set; } = new List<FoodIntake>();
    }
}
