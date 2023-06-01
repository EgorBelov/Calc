using CaclApi.DAL;
using CaclApi.DAL.Entities;
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
    public class IndexModel : GetConstantListPage
    {
        private readonly IFoodIntakeService _foodIntakesService;

        public IndexModel(IFoodIntakeService foodIntakesService)
        {
            _foodIntakesService = foodIntakesService;
        }
        public List<FoodIntake> FoodIntakes { get; set; }
        //public List<Ingredient> Ingredient { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchText { get; set; }
        public async Task<IActionResult> OnGetAsync(CancellationToken ct)
        {
            try
            {
                var foodIntakes = await _foodIntakesService.GetFoodIntakes(ct);
                FoodIntakes = foodIntakes;
                foreach (var food in FoodIntakes)
                {
                    foreach (var meal in food.Meals) meal.MealTotal();
                    
                    food.FoodIntakeTotal();
                }
                // ���������� ����������
                if (!string.IsNullOrEmpty(SearchText))
                {
                    foodIntakes = foodIntakes.Where(f => f.FoodIntakeType.Name.ToLower().Contains(SearchText) ||
                                                         f.Date.ToString().ToLower().Contains(SearchText) ||
                                                         f.Meals.Any(m => m.Name.ToLower().Contains(SearchText)) ||
                                                         f.TotalCalories.ToString().ToLower().Contains(SearchText))
                                             .ToList();
                }
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Account/Login");
            }
        }

        public async Task<IActionResult> OnPostDelete(int? id, CancellationToken ct)
        {
            var res = await _foodIntakesService.DeleteFoodIntake(id, ct);

            if (res)
                return RedirectToPage("Index");
            else
                return NotFound();
        }


    }
}
