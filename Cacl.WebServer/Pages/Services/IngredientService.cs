using CaclApi.DAL.Entities;
using CaclApi.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CaclApi.Pages.Services
{
    public interface IIngredientService
    {
        Task<List<Ingredient>> GetIngredients(CancellationToken ct);
        Task<bool> DeleteIngredient(int? id, CancellationToken ct);
        Task<Ingredient> GetIngredient(int id, CancellationToken ct);
    }
    public class IngredientService : IIngredientService
    {

        private readonly CalcApiContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public IngredientService(CalcApiContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> DeleteIngredient(int? id, CancellationToken ct)
        {
            if (id == null)
                return false;

            var ingredient = await _context.Ingredients
                .Include(x => x.Product).ThenInclude(x => x.ProductCategory)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (ingredient == null)
                return false;

            _context.Ingredients.Remove(ingredient);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Ingredient> GetIngredient(int id, CancellationToken ct)
        {
            var ingredient = await _context.Ingredients
               .Include(x => x.Product)
               .FirstOrDefaultAsync(x => x.Id == id);


            return ingredient;
        }

        public async Task<List<Ingredient>> GetIngredients(CancellationToken ct)
        {
            var ingredients = await _context.Ingredients
                .Include(x => x.Product).ThenInclude(x => x.ProductCategory)
                //.Where(x => x.UserId == user.Id)
                .ToListAsync();
            foreach(var item in ingredients)
            {
                item.IngredientTotal();
            }

            return ingredients;
        }
    }
}
