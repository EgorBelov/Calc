using CaclApi.DAL.Entities;
using CaclApi.DAL;
using CaclApi.Pages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CaclApi.Pages
{
    [Authorize]
    public class EditModel : GetConstantListPage
    {
        private readonly CalcApiContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFoodIntakeService _foodIntakesService;


        [BindProperty]
        public FoodIntake FoodIntake { get; set; }

        [BindProperty]
        public List<SelectListItem> MealList { get; set; }

        [BindProperty]
        public List<int> SelectedMealIds { get; set; }
        public EditModel(CalcApiContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IFoodIntakeService foodIntakesService)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _foodIntakesService = foodIntakesService;

        }
        public async Task<IActionResult> OnGetAsync(int? id, CancellationToken ct)
        {
            if (id == null)
                return NotFound();

            try
            {
                FoodIntake = await _foodIntakesService.GetFoodIntake(id.Value, ct);
            }
            catch (Exception)
            {
                return RedirectToPage("/Account/Login");
            }


            if (FoodIntake == null)
                return NotFound();
            var meals = await _context.Meals.ToListAsync();
            MealList = meals.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();
            FoodIntakeTypeDropDownList(_context, FoodIntake.FoodIntakeTypeId);
          

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken ct)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

                if (user == null)
                    return RedirectToPage("/Account/Login");

                var foodIntake = await _foodIntakesService.GetFoodIntake(FoodIntake.Id, ct);

                if (foodIntake == null)
                    return NotFound();

                foodIntake.Meals.Clear();
                var selectedMeals = await _context.Meals
                    .Where(m => SelectedMealIds.Contains(m.Id))
                    .ToListAsync();
                FoodIntake.Meals = selectedMeals;
                FoodIntake.TotalCalories = 0;
                FoodIntake.FoodIntakeTotal();
                foodIntake.Meals = FoodIntake.Meals;
                foodIntake.Date = FoodIntake.Date;
                foodIntake.FoodIntakeTypeId = FoodIntake.FoodIntakeTypeId;

               
                await _context.SaveChangesAsync();

                return RedirectToPage("/Index");
            }

            FoodIntakeTypeDropDownList(_context);
          
            return Page();
        }
    }
}
