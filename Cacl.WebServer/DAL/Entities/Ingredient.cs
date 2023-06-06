namespace CaclApi.DAL.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public double? ProductQuantity { get; set; }
        public string? Description { get; set; }
        public int? ProductId { get; set; }      // внешний ключ
        public Product? Product { get; set; }    // навигационное свойство

        public List<Meal> Meals { get; set; } = new();


        public double TotalCalories = 0;

        public void IngredientTotal() 
        {
            this.TotalCalories = Product.Calories * (double)(ProductQuantity) * 0.1;
        }
    }
}
