using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using CaclApi.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CaclApi.DAL;

namespace CalcApi.Pages.Account
{
    public class RegisterModel : PageModel
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
            PhysicalActivityDropDownList();
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public int Age { get; set; }
        [BindProperty]
        public double Weight { get; set; }
        [BindProperty]
        public double Height { get; set; }
        [BindProperty]
        public string Sex { get; set; }
        //[BindProperty]
        //public PhysicalActivity PhysicalActivity { get; set; }

        public SelectList physicalActivitySL { get; set; }

        public void PhysicalActivityDropDownList(object value = null)
        {
            var query = _context.PhysicalActivities.OrderBy(x => x.Description);

            physicalActivitySL = new SelectList(query.AsNoTracking(),
                        "Id", "Description", value);
        }
        public string ReturnUrl { get; set; }



        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new User() { Email = Email, UserName = Email, Name = Name, Weight = Weight, Height = Height , Age = Age , Sex = Sex /*, PhysicalActivity = PhysicalActivity */};
                var result = await _userManager.CreateAsync(user, Password);

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
