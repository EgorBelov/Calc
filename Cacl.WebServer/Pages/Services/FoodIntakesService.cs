using CaclApi.DAL;
using CaclApi.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CaclApi.Pages.Services
{
    public interface IFoodIntakesService
    {
        Task<List<FoodIntake>> GetFoodIntakes(CancellationToken ct);
        Task<bool> DeleteFoodIntake(int? id, CancellationToken ct);
        Task<FoodIntake> GetFoodIntake(int id, CancellationToken ct);
    }

    public class FoodIntakesService: IFoodIntakesService
    {

        private readonly CalcApiContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FoodIntakesService(CalcApiContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

       //public async Task<List<Meal>> GetMealsInFoodIntake(int id, CancellationToken ct)
       // {

       //     var rents = GetFoodIntakes(ct);

       //     foreach (var meal in rents.Meals) { })


       // }


        public async Task<FoodIntake> GetFoodIntake(int id, CancellationToken ct)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
                throw new ArgumentException("Доступ закрыт");

            var foodIntake = await _context.FoodIntakes
                .Include(x => x.FoodIntakeType)
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
    }
}
