using CaclApi.DAL.Entities;
using CaclApi.Pages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CaclApi.Pages.Meals
{
    [Authorize]
    public class IndexModel : GetConstantListPage
    {
        private readonly IMealService _mealService;
        public List<int?> sumCalorie { get; set; }


        public IndexModel(IMealService mealService)
        {
            _mealService = mealService;

        }

        public List<Meal> Meals { get; set; }

        public async Task<IActionResult> OnGetAsync(CancellationToken ct)
        {
            try
            {
                var meals = await _mealService.GetMeals(ct);
                Meals = meals;
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Account/Login");
            }
        }

        public async Task<IActionResult> OnPostDelete(int? id, CancellationToken ct)
        {
            var res = await _mealService.DeleteMeal(id, ct);

            if (res)
                return RedirectToPage("Index");
            else
                return NotFound();
        }
    }
}
