using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using CaclApi.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CaclApi.DAL;
using CaclApi.Pages.Services;
using System.ComponentModel.DataAnnotations;

namespace CalcApi.Pages.Account
{
    public class RegisterModel : GetConstantListPage
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly CalcApiContext _context;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            CalcApiContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            PhysicalActivityDropDownList(_context);
        }


        public async Task<IActionResult> OnGetAsync()
        {
            PhysicalActivityDropDownList(_context);
            return Page();
        }



        [Required]
        [EmailAddress]
        [BindProperty]
        public string Email { get; set; }

        [Required]
        [BindProperty]
        public string Password { get; set; }

        [Required]
        [BindProperty]
        public string ConfirmPassword { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public int Age { get; set; }
        //[BindProperty]
        //public double Weight { get; set; }
        [BindProperty]
        public double Height { get; set; }
        [BindProperty]
        public string Sex { get; set; }
        [BindProperty]
        public int PhysicalActivityId { get; set; }

        //public SelectList physicalActivitySL { get; set; }
        //[BindProperty]
        //public User User { get; set; }
        
        public string ReturnUrl { get; set; }



        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {

                //var user = new User(Email, Password) {Name = User.Name, Age = User.Age, Height = User.Age, Sex = User.Sex, PhysicalActivity = User.PhysicalActivity };
                
                var meal = await _context.PhysicalActivities
                .FirstOrDefaultAsync(x => x.Id == PhysicalActivityId);

                var user = new User(Email, Password) { Name = Name, Age = Age, Height = Age, Sex = Sex, PhysicalActivity = meal };

                var result = await _userManager.CreateAsync(user, ConfirmPassword);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToPage("/Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
