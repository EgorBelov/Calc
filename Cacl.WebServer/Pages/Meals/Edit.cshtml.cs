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
    public class EditModel : GetConstantListPage
    {
        private readonly CalcApiContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMealService _mealService;


        [BindProperty]
        public Meal Meal { get; set; }


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

                meal.Name = Meal.Name;
                


                await _context.SaveChangesAsync();

                return RedirectToPage("/Index");
            }

            MealCategoryDropDownList(_context);

            return Page();
        }
    }
}
