using CaclApi.DAL;
using CaclApi.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CaclApi.Pages.Services
{
    public interface IMealService
    {
        Task<List<Meal>> GetMeals(CancellationToken ct);
        Task<bool> DeleteMeal(int? id, CancellationToken ct);
        Task<Meal> GetMeal(int id, CancellationToken ct);
    }
    public class MealService : IMealService
    {

        private readonly CalcApiContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public MealService(CalcApiContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) 
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> DeleteMeal(int? id, CancellationToken ct)
        {
            if (id == null)
                return false;

            var meal = await _context.Meals
                .Include(x => x.MealCategory)
                .Include(x => x.Ingredients).ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (meal == null)
                return false;

            _context.Meals.Remove(meal);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Meal> GetMeal(int id, CancellationToken ct)
        {
            var meal = await _context.Meals
               .Include(x => x.MealCategory)
               .Include(x => x.Ingredients).ThenInclude(x => x.Product)
               //.Where(x => x.UserId == user.Id)
               .FirstOrDefaultAsync(x => x.Id == id);


            return meal;
        }

        public async Task<List<Meal>> GetMeals(CancellationToken ct)
        {
            //var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            //if (user == null)
            //    throw new ArgumentException("Доступ закрыт");

            var meals = await _context.Meals
                .Include(x => x.MealCategory)
                .Include(x => x.Ingredients).ThenInclude(x => x.Product)
                //.Where(x => x.UserId == user.Id)
                .ToListAsync();
            foreach(var item in meals)
            {
                foreach(var meal in item.Ingredients)
                {
                    meal.IngredientTotal();
                }
                item.MealTotal();
            }

            return meals;
        }
    }
}
