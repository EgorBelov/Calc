namespace CaclApi.DAL.Entities
{
    public class Meal
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? MealCategoryId { get; set; }     
        public MealCategory? MealCategory { get; set; }   
        public List<Ingredient> Ingredients { get; set; } = new ();
        public List<FoodIntake> FoodIntakes { get; set; } = new ();

        public int TotalCalories { get; set; } = 0;
        
        public void MealTotal()
        {
            foreach(var item in Ingredients)
            {
                this.TotalCalories += item.TotalCalories;
            }
        }
    }
}
