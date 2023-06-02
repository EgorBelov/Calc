using CaclApi.DAL;
using CaclApi.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CaclApi.Pages.Services
{
    public interface IFoodIntakeService
    {
        Task<List<FoodIntake>> GetFoodIntakes(CancellationToken ct);
        Task<bool> DeleteFoodIntake(int? id, CancellationToken ct);
        Task<FoodIntake> GetFoodIntakeById(int id, CancellationToken ct);
        Task CreateFoodIntake(FoodIntake foodIntake);
        Task UpdateFoodIntake(FoodIntake foodIntake);
        Task<FoodIntake> GetFoodIntake(int id, CancellationToken ct);
    }

    public class FoodIntakeService: IFoodIntakeService
    {

        private readonly CalcApiContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public async Task<bool> DeleteFoodIntake(int id)
        {
            var existingFoodIntake = await _context.FoodIntakes.FindAsync(id);

            if (existingFoodIntake == null)
                return false;

            _context.FoodIntakes.Remove(existingFoodIntake);
            await _context.SaveChangesAsync();

            return true;
        }

        public FoodIntakeService(CalcApiContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        //public async Task<List<Ingredient>> GetMealsInFoodIntake(int id, CancellationToken ct)
        // {

        //     var rents = GetFoodIntakes(ct);

        //     foreach (var meal in rents.Ingredient) { })


        // }
        public async Task<FoodIntake> GetFoodIntakeById(int id, CancellationToken ct)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
                throw new ArgumentException("Access denied");

            var foodIntake = await _context.FoodIntakes
                .Include(x => x.FoodIntakeType)
                .Include(x => x.Meals)
                .Where(x => x.UserId == user.Id)
                .FirstOrDefaultAsync(x => x.Id == id, ct);

            return foodIntake;
        }

        public async Task<FoodIntake> GetFoodIntake(int id, CancellationToken ct)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
                throw new ArgumentException("Доступ закрыт");

            var foodIntake = await _context.FoodIntakes
                .Include(x => x.FoodIntakeType)
                .Include(x => x.Meals)
                .Where(x => x.UserId == user.Id)
                .FirstOrDefaultAsync(x => x.Id == id) ;


            return foodIntake;

        }

        public  async Task<List<FoodIntake>> GetFoodIntakes(CancellationToken ct)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
                throw new ArgumentException("Доступ закрыт");

            var foodIntakes = await _context.FoodIntakes
                .Include(x => x.FoodIntakeType)
                .Include(x => x.Meals)
                .Where(x => x.UserId == user.Id)
                .ToListAsync();


            return foodIntakes;
            
        }

        public async Task<bool> DeleteFoodIntake(int? id, CancellationToken ct)
        {
            if (id == null)
                return false;

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            
            var foodIntake = await _context.FoodIntakes
                .Include(x => x.FoodIntakeType)
                .Where(x => x.UserId == user.Id)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (foodIntake == null)
                return false;

            _context.FoodIntakes.Remove(foodIntake);

            await _context.SaveChangesAsync();

            return true;
        }


        public async Task CreateFoodIntake(FoodIntake foodIntake)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
                throw new ArgumentException("Access denied");

            foodIntake.UserId = user.Id;
            foodIntake.Date = DateTime.Now;

            _context.FoodIntakes.Add(foodIntake);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFoodIntake(FoodIntake foodIntake)
        {
            var existingFoodIntake = await _context.FoodIntakes.FindAsync(foodIntake.Id);

            if (existingFoodIntake == null)
                throw new ArgumentException("Food intake not found");

            existingFoodIntake.FoodIntakeTypeId = foodIntake.FoodIntakeTypeId;
            existingFoodIntake.FoodIntakeType = foodIntake.FoodIntakeType;
            existingFoodIntake.Meals = foodIntake.Meals;
            existingFoodIntake.TotalCalories = foodIntake.TotalCalories;

            _context.FoodIntakes.Update(existingFoodIntake);
            await _context.SaveChangesAsync();
        }

    }
}
