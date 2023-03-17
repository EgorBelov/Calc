namespace CaclApi.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public int? Age { get; set; }
        public string? Sex { get; set; }
        public List<FoodIntake>? FoodIntakes { get; set; } = new();
        public UserLogin UserLogin { get; set; }
        public List<WeightDiary>? WeightDiaries { get; set; } = new();
        public int PhysicalActivityId { get; set; }
        public PhysicalActivity PhysicalActivity { get; set;}
    }
}
