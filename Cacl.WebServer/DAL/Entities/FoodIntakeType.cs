namespace CaclApi.DAL.Entities
{
    public class FoodIntakeType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<FoodIntake> FoodIntake { get; set; }
    }
}
