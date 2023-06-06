using CaclApi.DAL.Entities;
using System;

namespace CaclApi.DAL
{
    public class CalcApiContextSeed
    {
        public static async Task InitializeDb(CalcApiContext db)
        {
            var physicalActivity1 = new PhysicalActivity
            {
                Description = "Walking",
                Ratio = 1.3,
            };

            var physicalActivity2 = new PhysicalActivity
            {
                Description = "Running",
                Ratio = 1.7,
            };

            var physicalActivity3 = new PhysicalActivity
            {
                Description = "Swimming",
                Ratio = 1.8,
            };

            var physicalActivity4 = new PhysicalActivity
            {
                Description = "Cycling",
                Ratio = 1.5,
            };


            // Create a list of User objects
            List<User> users = new List<User>
            {
                new User("e1@mail.ru", "123")
                {
                    Name = "John Doe",
                    Weight = 75.0,
                    Height = 1.8,
                    Age = 30,
                    Sex = "Male",
                    PhysicalActivity = physicalActivity1
                },
                new User("e2@mail.ru", "123")
                {
                    Name = "Jane Smith",
                    Weight = 62.5,
                    Height = 1.65,
                    Age = 25,
                    Sex = "Female",
                    PhysicalActivity = physicalActivity3
                }
            };

            // Create a list of WeightDiary objects
            List<WeightDiary> weightDiaries = new List<WeightDiary>
            {
                new WeightDiary(DateTime.Now, 74.5, users[0]),                           
                new WeightDiary(DateTime.Now, 62.5, users[1]),
            };

            // Create a list of ProductCategory objects
            List<ProductCategory> productCategories = new List<ProductCategory>
            {
                new ProductCategory
                {
                    Name = "Fruits",
                    Description = "Fresh fruits"
                },
                new ProductCategory
                {
                    Name = "Vegetables",
                    Description = "Fresh vegetables"
                },
                new ProductCategory
                {
                    Name = "Dairy",
                    Description = "Milk, cheese, yogurt"
                },
                new ProductCategory
                {
                    Name = "Porridge",
                    Description = "Porridge"
                },
                new ProductCategory
                {
                    Name = "Nuts",
                    Description = "Nuts"
                }
            };

            // Create a list of Product objects
            List<Product> products = new List<Product>
            {
                new Product
                {
                    Name = "Apple",
                    Calories = 52,
                    ProductCategory = productCategories[0]
                },
                new Product
                {
                    Name = "Banana",
                    Calories = 89,
                    ProductCategory = productCategories[0]
                },
                new Product
                {
                    Name = "Carrot",
                    Calories = 41,
                   ProductCategory = productCategories[1]
                },
                new Product
                {
                    Name = "Broccoli",
                    Calories = 55,
                    ProductCategory = productCategories[1]
                },
                new Product
                {
                    Name = "Milk",
                    Calories = 42,
                    ProductCategory = productCategories[2]
                },
                new Product
                {
                    Name = "Oatmeal",
                    Calories = 113,
                    ProductCategory = productCategories[3]
                },
                new Product
                {
                    Name = "Almonds",
                    Calories = 154,
                    ProductCategory = productCategories[4]
                }
            };

            List<Ingredient> ingredients = new List<Ingredient>
            {
                new Ingredient
                {
                    Product = products[0],
                    ProductQuantity = 1,
                    Description = "One medium-sized apple"
                },
                new Ingredient
                {
                    Product = products[1],
                    ProductQuantity = 1,
                    Description = "One medium-sized banana"
                },
                new Ingredient
                {
                    Product = products[2],
                    ProductQuantity = 100,
                    Description = "100g of carrots"
                },
                new Ingredient
                {
                    Product = products[3],
                    ProductQuantity = 100,
                    Description = "100g of broccoli"
                },

                new Ingredient
                {
                    Product = products[4],
                    ProductQuantity = 200,
                    Description = "200ml of milk"
                },
                new Ingredient
                {
                    Product = products[4],
                    ProductQuantity = 100,
                    Description = "100ml of almond milk"
                },
                new Ingredient
                {
                    Product = products[5],
                    ProductQuantity = 50,
                    Description = "50g of oatmeal"
                },
                new Ingredient
                {
                    Product = products[6],
                    ProductQuantity = 20,
                    Description = "20g of almonds"
                },
                new Ingredient
                {
                    Product = products[6],
                    ProductQuantity = 30,
                    Description = "30g of almonds"
                }
            };

            for (int i = 0; i < ingredients.Count(); i++)
            {
                ingredients[i].IngredientTotal();
            }


            List<MealCategory> mealsCategories = new List<MealCategory>
            {
                new MealCategory
                {
                    Name = "Vegetarian meals", Description = "Vegetarian meals"
                },
                new MealCategory
                {
                    Name = "Gluten-free meals", Description = "Gluten-free meals"
                },
                new MealCategory
                {
                    Name = "Low-carb meals", Description = "Low-carb meals"
                },
                new MealCategory
                {
                    Name = "High-protein meals", Description = "High-protein meals"
                },
                new MealCategory
                {
                    Name = "Desserts", Description = "Just a Desserts"
                }
            };

            List<Meal> meals = new List<Meal>
            {
                new Meal
                {
                    Name = "Oatmeal Breakfast",
                    MealCategory = mealsCategories[0],
                    Ingredients = new List<Ingredient> {ingredients[0], ingredients[5], ingredients[6] }
                },
                new Meal
                {
                    Name = "Banana Smoothie",
                    MealCategory = mealsCategories[1],
                    Ingredients = new List<Ingredient> {ingredients[1], ingredients[4] }
                },
                new Meal
                {
                    Name = "Carrot and Broccoli Salad",
                    MealCategory = mealsCategories[2],
                    Ingredients = new List<Ingredient> {ingredients[2], ingredients[3], ingredients[7] }
                },
                new Meal
                {
                    Name = "Apple and Carrot Smoothie",
                    MealCategory = mealsCategories[3],
                    Ingredients = new List<Ingredient> {ingredients[0], ingredients[2], ingredients[5] }
                },
                new Meal
                {
                    Name = "Broccoli and Almond Soup",
                    MealCategory = mealsCategories[4],
                    Ingredients = new List<Ingredient> {ingredients[3], ingredients[5], ingredients[8] }
                }
            };
            for (int i = 0; i < meals.Count(); i++)
            {
                meals[i].MealTotal();
            }
            List<FoodIntakeType> foodIntakeTypes = new List<FoodIntakeType>
            {
                new FoodIntakeType
                {
                    Name = "Breakfast", Description = "First meal of the day"
                },
                new FoodIntakeType
                {
                    Name = "Afternoon snack", Description = "An unexpected afternoon snack"
                },
                new FoodIntakeType
                {
                    Name = "Lunch", Description = "A hearty lunch"
                },
                new FoodIntakeType
                {
                    Name = "Dinner", Description = "Delicious dinner"
                },
                new FoodIntakeType
                {
                    Name = "Snack", Description = "Just a snack"
                }
            };

            List<FoodIntake> foodIntakes = new List<FoodIntake>
            {
                new FoodIntake
                {
                    FoodIntakeType = foodIntakeTypes[0],
                    Date = DateTime.Now,
                    User = users[0],
                    Meals = new List<Meal> {meals[0], meals[1] }
                },
                new FoodIntake
                {
                    FoodIntakeType = foodIntakeTypes[4],
                    Date = DateTime.Now,
                    User = users[0],
                    Meals = new List<Meal> {meals[4]}
                },
                new FoodIntake
                {
                    FoodIntakeType = foodIntakeTypes[1],
                    Date = DateTime.Now,
                    User = users[0],
                    Meals = new List<Meal> {meals[4], meals[2], meals[3] }
                },
                new FoodIntake
                {
                    FoodIntakeType = foodIntakeTypes[2],
                    Date = DateTime.Now,
                    User = users[1],
                    Meals = new List<Meal> {meals[2]}
                },
                new FoodIntake
                {
                    FoodIntakeType = foodIntakeTypes[3],
                    Date = DateTime.Now,
                    User = users[1],
                    Meals = new List<Meal> {meals[1], meals[3] }
                },
                new FoodIntake
                {
                    FoodIntakeType = foodIntakeTypes[4],
                    Date = DateTime.Now,
                    User = users[1],
                    Meals = new List<Meal> {meals[0], meals[1], meals[2], meals[3], meals[4]}
                }
            };

            for (int i = 0; i < foodIntakes.Count(); i++)
            {
                foodIntakes[i].FoodIntakeTotal();
            }



            await db.FoodIntakes.AddRangeAsync(foodIntakes);
            await db.PhysicalActivities.AddRangeAsync(physicalActivity1, physicalActivity2, physicalActivity3, physicalActivity4);
            await db.WeightDiaries.AddRangeAsync(weightDiaries);
            await db.SaveChangesAsync();
        }
    }
}
