using CaclApi.DAL.Entities;
using CaclApi.DAL;
using CaclApi.Pages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CaclApi.Pages.Ingredients
{
    [Authorize]
    public class EditModel : GetConstantListPage
    {
        private readonly CalcApiContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIngredientService _ingredientService;


        [BindProperty]
        public Ingredient Ingredient { get; set; }


        public EditModel(CalcApiContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IIngredientService ingredientService)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _ingredientService = ingredientService;

        }
        public async Task<IActionResult> OnGetAsync(int? id, CancellationToken ct)
        {
            if (id == null)
                return NotFound();

            try
            {
                Ingredient = await _ingredientService.GetIngredient(id.Value, ct);
            }
            catch (Exception)
            {
                return RedirectToPage("/Account/Login");
            }


            if (Ingredient == null)
                return NotFound();


            ProductDropDownList(_context, Ingredient.ProductId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken ct)
        {
            if (ModelState.IsValid)
            {
               

                var ingredient = await _ingredientService.GetIngredient(Ingredient.Id, ct);

                if (ingredient == null)
                    return NotFound();

                ingredient.Product = Ingredient.Product;
                ingredient.ProductId = Ingredient.ProductId;
                ingredient.ProductQuantity = Ingredient.ProductQuantity;
                ingredient.Description = Ingredient.Description;


                await _context.SaveChangesAsync();

                return RedirectToPage("/Index");
            }

            ProductDropDownList(_context);

            return Page();
        }
    }
}
