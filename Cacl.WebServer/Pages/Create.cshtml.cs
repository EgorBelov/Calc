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
    public class CreateModel : GetConstantListPage
    {
        private readonly CalcApiContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
     

        [BindProperty]
        public FoodIntake FoodIntake { get; set; }

        

        //[BindProperty]
        //public List<Meal> SelectedMeals { get; set; }

        public CreateModel(CalcApiContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

        }


        public IActionResult OnGet()
        {
            FoodIntakeTypeDropDownList(_context);
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                    FoodIntake.User = user;
                    
                   //foreach(var meal in SelectedMeals) FoodIntake.Meals.Add(meal);
                    
                    await _context.FoodIntakes.AddAsync(FoodIntake); 
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Index");
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
