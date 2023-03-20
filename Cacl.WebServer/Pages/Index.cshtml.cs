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
    public class IndexModel : PageModel
    {
        private readonly IFoodIntakesService _foodIntakesService;

        public IndexModel(IFoodIntakesService foodIntakesService)
        {
            _foodIntakesService = foodIntakesService;
        }
        public List<FoodIntake> FoodIntakes { get; set; }
        public List<Meal> Meals { get; set; }
        public async Task<IActionResult> OnGetAsync(CancellationToken ct)
        {
            try
            {
                var foodIntakes = await _foodIntakesService.GetFoodIntakes(ct);
                FoodIntakes = foodIntakes;
                
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
