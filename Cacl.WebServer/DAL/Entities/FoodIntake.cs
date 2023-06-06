namespace CaclApi.DAL.Entities
{
    public class FoodIntake
    {
        public int Id { get; set; }
        public int? FoodIntakeTypeId { get; set; }      
        public FoodIntakeType? FoodIntakeType { get; set; }    
        public DateTime? Date { get; set; }
        public List<Meal> Meals { get; set; } = new();
        public string? UserId { get; set; }    
        public User? User { get; set; }   


        public double TotalCalories { get; set; } = 0;

        public void FoodIntakeTotal ()
        {
            this.TotalCalories = 0;
            foreach (var item in Meals)
            {
                this.TotalCalories += item.TotalCalories;
            }
        }
    }
}
