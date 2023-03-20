using CaclApi.DAL.Entities;

namespace CaclApi.DAL
{
    public class CalcApiContextSeed
    {
        public static async Task InitializeDb(CalcApiContext db)
        {
            //var productCategory1 = new ProductCategory() { Name = "Фрукт", Description = "qweqwrqwrq"};
            //var productCategory2 = new ProductCategory() { Name = "Овощ", Description = "qweqwrqwrq" };
            //var productCategory3 = new ProductCategory() { Name = "Молочка", Description = "qweqwrqwrq" };

            //var product1 = new Product() { Name = "Яблоко", Calories = 150, ProductCategory = productCategory1 };
            //var product2 = new Product() { Name = "Огурец", Calories = 10, ProductCategory = productCategory2 };
            //var product3 = new Product() { Name = "Йогурт", Calories = 247, ProductCategory = productCategory3 };

            //var ingredient1 = new Ingredient() { Product = product1, ProductQuantity = 500, Description = "Для блюда" };
            //var ingredient2 = new Ingredient() { Product = product2, ProductQuantity = 100, Description = "Тоже для блюда" };


            //var mealCategory = new MealCategory() { Name = "НЕ СЪЕДОБНО", Description = "Шпоаф" };

            //var meal = new Ingredient() { Name = "Яблоко с огурцом", MealCategory= mealCategory };

            //meal.Ingredient?.Add(ingredient1);
            //meal.Ingredient?.Add(ingredient2);

            //var foodintakesType1 = new FoodIntakeType() { Name = "Завтрак", Description = "Вкусно" };
            //var foodintakesType2 = new FoodIntakeType() { Name = "Полдник", Description = "Вкусно" };
            //var foodintakesType3 = new FoodIntakeType() { Name = "Обед", Description = "Вкусно" };
            //var foodintakesType4 = new FoodIntakeType() { Name = "Ужин", Description = "Вкусно" };
            //var foodintakesType5 = new FoodIntakeType() { Name = "Просто перекус", Description = "Вкусно" };


            //var foodintake = new Ingredient() { FoodIntakeType= foodintakesType1, Date = DateTime.Now };

            //foodintake.Ingredient?.Add(meal);

            //var physicalActivity = new PhysicalActivity() { Description = "много тренировок", Ratio = 1.65 };
            
            //var user = new User("egor@mail.ru", "123456") { Name = "Егор", Height = 180, Weight = 70, Age = 19, Sex = "male", PhysicalActivity = physicalActivity };

            //user.FoodIntakes?.Add(foodintake);

            ////var userLogin = new User("egor@mail.ru", "123456") { User = User };

            //var weightDiary = new WeightDiary() { User = user, CurrentWeight = 70, Date = DateOnly.FromDateTime(DateTime.Now) };

            //await db.FoodIntakeTypes.AddRangeAsync(foodintakesType2,foodintakesType3,foodintakesType4,foodintakesType5);
            ////await db.UserLogins.AddAsync(userLogin);
            //await db.FoodIntakes.AddAsync(foodintake);
            //await db.Ingredient.AddRangeAsync(ingredient1, ingredient2);
            //await db.WeightDiaries.AddAsync(weightDiary);
            //await db.Ingredient.AddAsync(meal);

            //await db.SaveChangesAsync();
        }
    }
}
