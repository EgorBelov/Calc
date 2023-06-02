using CaclApi.DAL;
using CaclApi.DAL.Entities;
using CaclApi.Pages.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace TestProjectCalcApi
{
    public class FoodIntakeServiceTests
    {
        private CalcApiContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<CalcApiContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            return new CalcApiContext(options);
        }

        private IHttpContextAccessor CreateHttpContextAccessor()
        {
            // TODO: Implement a mock or fake IHttpContextAccessor for testing
            return null;
        }

        private UserManager<User> CreateUserManager()
        {
            // TODO: Implement a mock or fake UserManager<User> for testing
            return null;
        }

        [Fact]
        public async Task TestFoodIntakeServiceCRUD()
        {
            using (var context = CreateDbContext())
            {
                var userManager = CreateUserManager();
                var httpContextAccessor = CreateHttpContextAccessor();
                var foodIntakeService = new FoodIntakeService(context, userManager, httpContextAccessor);

                // Create a food intake
                var foodIntake = new FoodIntake
                {
                    FoodIntakeTypeId = 1,
                    Meals = new List<Meal> { new Meal { Name = "Breakfast", TotalCalories = 500 } }
                };
                await foodIntakeService.CreateFoodIntake(foodIntake);

                // Get all food intakes
                var foodIntakes = await foodIntakeService.GetFoodIntakes(default);
                Assert.Single(foodIntakes);

                // Get the created food intake by ID
                var createdFoodIntake = await foodIntakeService.GetFoodIntakeById(foodIntake.Id, default);
                Assert.NotNull(createdFoodIntake);
                Assert.Equal(1, createdFoodIntake.FoodIntakeTypeId);

                // Update the food intake
                createdFoodIntake.FoodIntakeTypeId = 2;
                await foodIntakeService.UpdateFoodIntake(createdFoodIntake);

                // Verify the updated food intake
                var updatedFoodIntake = await foodIntakeService.GetFoodIntakeById(foodIntake.Id, default);
                Assert.Equal(2, updatedFoodIntake.FoodIntakeTypeId);

                // Delete the food intake
                await foodIntakeService.DeleteFoodIntake(foodIntake.Id);

                // Verify that the food intake has been deleted
                var deletedFoodIntake = await foodIntakeService.GetFoodIntakeById(foodIntake.Id, default);
                Assert.Null(deletedFoodIntake);
            }
        }
    }
}
