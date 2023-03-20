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
    public class CreateModel : GetConstantListPage
    {
        private readonly CalcApiContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        [BindProperty]
        public Ingredient Ingredient { get; set; }

        public CreateModel(CalcApiContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

        }


        public IActionResult OnGet()
        {
            ProductDropDownList(_context);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Ingredients.AddAsync(Ingredient);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Ingredients/Index");
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
