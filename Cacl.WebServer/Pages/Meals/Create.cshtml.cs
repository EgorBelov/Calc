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
    public class CreateModel : GetConstantListPage
    {

        private readonly CalcApiContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        [BindProperty]
        public Meal Meal { get; set; }



        [BindProperty]
        public List<SelectListItem> IngredientList { get; set; }

        [BindProperty]
        public List<int> SelectedIngredientIds { get; set; }

        public CreateModel(CalcApiContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

        }


        public async Task<IActionResult> OnGetAsync()
        {
            IngredientDropDownList(_context);
            MealCategoryDropDownList(_context);

            var ingredients = await _context.Ingredients.Include(x => x.Product).ToListAsync();
            IngredientList = ingredients.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Product.Name + " " + m.ProductQuantity.ToString(),
            }).ToList();

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                    //FoodIntake.User = user;

                    var selectedIngredients = await _context.Ingredients
                    .Where(m => SelectedIngredientIds.Contains(m.Id))
                    .ToListAsync();

                    Meal.Ingredients = selectedIngredients;
                    Meal.MealTotal();
                    await _context.Meals.AddAsync(Meal);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Meals/Index");
                }
                catch (Exception)
                {
                    return RedirectToPage("/Account/Login");
                }
            }
            return Page();
        }
    }
}
