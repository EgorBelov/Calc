using CaclApi.DAL.Entities;
using CaclApi.DAL;
using CaclApi.Pages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CaclApi.Pages.Meals
{
    [Authorize]
    public class EditModel : GetConstantListPage
    {
        private readonly CalcApiContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMealService _mealService;


        [BindProperty]
        public Meal Meal { get; set; }
        [BindProperty]
        public List<SelectListItem> IngredientList { get; set; }

        [BindProperty]
        public List<int> SelectedIngredientIds { get; set; }

        public EditModel(CalcApiContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IMealService mealService)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _mealService = mealService;

        }
        public async Task<IActionResult> OnGetAsync(int? id, CancellationToken ct)
        {
            if (id == null)
                return NotFound();

            try
            {
                Meal = await _mealService.GetMeal(id.Value, ct);
            }
            catch (Exception)
            {
                return RedirectToPage("/Account/Login");
            }


            if (Meal == null)
                return NotFound();

            MealCategoryDropDownList(_context, Meal.MealCategoryId);
            var ingredients = await _context.Ingredients.Include(x => x.Product).ToListAsync();
            IngredientList = ingredients.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Product.Name + " " + m.ProductQuantity.ToString(),
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken ct)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

                if (user == null)
                    return RedirectToPage("/Account/Login");

                var meal = await _mealService.GetMeal(Meal.Id, ct);

                if (meal == null)
                    return NotFound();

                meal.Ingredients.Clear();
                var selectedIngredients = await _context.Ingredients
                    .Where(m => SelectedIngredientIds.Contains(m.Id))
                    .ToListAsync();

                Meal.Ingredients = selectedIngredients;
                Meal.MealTotal();
                meal.TotalCalories = Meal.TotalCalories;
                meal.Ingredients = Meal.Ingredients;
                meal.Name = Meal.Name;
                


                await _context.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            MealCategoryDropDownList(_context);

            return Page();
        }
    }
}
