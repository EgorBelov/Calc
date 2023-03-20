using CaclApi.DAL.Entities;
using CaclApi.Pages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CaclApi.Pages.Ingredients
{
    [Authorize]
    public class IndexModel : GetConstantListPage
    {
        private readonly IIngredientService _ingredientService;

        public IndexModel(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public List<Ingredient> Ingredients { get; set; }

        public async Task<IActionResult> OnGetAsync(CancellationToken ct)
        {
            try
            {
                var ingredient = await _ingredientService.GetIngredients(ct);
                Ingredients = ingredient;

                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Account/Login");
            }
        }

        public async Task<IActionResult> OnPostDelete(int? id, CancellationToken ct)
        {
            var res = await _ingredientService.DeleteIngredient(id, ct);

            if (res)
                return RedirectToPage("Index");
            else
                return NotFound();
        }
    }
}
