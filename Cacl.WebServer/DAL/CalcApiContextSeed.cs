using CaclApi.DAL.Entities;

namespace CaclApi.DAL
{
    public class CalcApiContextSeed
    {
        public static async Task InitializeDb(CalcApiContext db)
        {
            var productCategory1 = new ProductCategory() { Name = "Фрукт", Description = "qweqwrqwrq"};
            var productCategory2 = new ProductCategory() { Name = "Овощ", Description = "qweqwrqwrq" };
            var productCategory3 = new ProductCategory() { Name = "Молочка", Description = "qweqwrqwrq" };

            var product1 = new Product() { Name = "Яблоко", Calories = 150, ProductCategory = productCategory1 };
            var product2 = new Product() { Name = "Огурец", Calories = 10, ProductCategory = productCategory2 };
            var product3 = new Product() { Name = "Йогурт", Calories = 247, ProductCategory = productCategory3 };

            var ingredient1 = new Ingredient() { Product = product1, ProductQuantity = 500, Description = "Для блюда" };
            var ingredient2 = new Ingredient() { Product = product2, ProductQuantity = 100, Description = "Тоже для блюда" };


            var mealCategory = new MealCategory() { Name = "НЕ СЪЕДОБНО", Description = "Шпоаф" };

            var meal = new Meal() { Name = "Яблоко с огурцом", MealCategory= mealCategory };

            meal.Ingredients?.Add(ingredient1);
            meal.Ingredients?.Add(ingredient2);

            var foodintakesType = new FoodIntakeType() { Name = "Завтрак", Description = "Вкусно" };

            var foodintake = new FoodIntake() { FoodIntakeType= foodintakesType, Date = DateTime.Now };

            foodintake.Meals?.Add(meal);

            var physicalActivity = new PhysicalActivity() { Description = "много тренировок", Ratio = 1.65 };
            
            var user = new User() { Name = "Егор", Height = 180, Weight = 70, Age = 19, Sex = "male", PhysicalActivity = physicalActivity};

            var userLogin = new UserLogin() { User = user, Login = "egor", Password = "123456" };

            var weightDiary = new WeightDiary() { User = user, CurrentWeight = 70, Date = DateOnly.FromDateTime(DateTime.Now) };


            await db.UserLogins.AddAsync(userLogin);
            await db.FoodIntakes.AddAsync(foodintake);
            await db.Ingredients.AddRangeAsync(ingredient1, ingredient2);
            await db.WeightDiaries.AddAsync(weightDiary);
            await db.Meals.AddAsync(meal);

            await db.SaveChangesAsync();
        }
    }
}
