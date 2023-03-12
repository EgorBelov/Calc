namespace CaclApi.DAL.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int? Weight { get; set; }
        public int? Height { get; set; }
        public int? Age { get; set; }
        public string? Sex { get; set; }
        public List<FoodIntake>? FoodIntakes { get; set; } = new();
        public UserLogin UserLogin { get; set; }
        public List<WeightDiary>? WeightDiaries { get; set; } = new();
        public Guid PhysicalActivityId { get; set; }
        public PhysicalActivity PhysicalActivity { get; set;}
    }
}
