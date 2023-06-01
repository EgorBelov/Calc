using CaclApi.DAL.Entities;
using CaclApi.Pages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CaclApi.Pages.Products
{
    [Authorize]
    public class IndexModel : GetConstantListPage
    {
        private readonly IProductService _ingredientService;

        public IndexModel(IProductService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public List<Product> Ingredients { get; set; }

        public async Task<IActionResult> OnGetAsync(CancellationToken ct)
        {
            try
            {
                var ingredient = await _ingredientService.GetProducts(ct);
                Ingredients = ingredient;

                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Account/Login");
            }
        }

      
    }
}
