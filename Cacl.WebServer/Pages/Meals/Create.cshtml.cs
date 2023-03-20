using CaclApi.DAL.Entities;
using CaclApi.DAL;
using CaclApi.Pages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public List<Product> SelectedProducts { get; set; }

        public CreateModel(CalcApiContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

        }


        public IActionResult OnGet()
        {
            IngredientDropDownList(_context);
            MealCategoryDropDownList(_context);
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                    //Ingredient.User = user;

                    //foreach (var meal in SelectedProducts) Ingredient.Ingredient.Add(meal);

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
