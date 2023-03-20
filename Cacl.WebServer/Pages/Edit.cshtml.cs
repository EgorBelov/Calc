using CaclApi.DAL.Entities;
using CaclApi.DAL;
using CaclApi.Pages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CaclApi.Pages
{
    [Authorize]
    public class EditModel : GetConstantListPage
    {
        private readonly CalcApiContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFoodIntakesService _foodIntakesService;


        [BindProperty]
        public FoodIntake FoodIntake { get; set; }


        public EditModel(CalcApiContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IFoodIntakesService foodIntakesService)
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
