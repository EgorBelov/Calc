using CaclApi.DAL.Entities;
using CaclApi.DAL;
using CaclApi.Pages.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CaclApi.Pages.WeightDiaries
{
    public class CreateModel : GetConstantListPage
    {
        private readonly CalcApiContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        [BindProperty]
        public WeightDiary WeightDiary { get; set; }


        public CreateModel(CalcApiContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> OnGetAsync()
        {
           

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //�������� ������������ ������������� �������� � ��������� ������
            if (ModelState.IsValid)
            {
                try
                {
                    // ��������� ���������� � ������� �������������� ������������
                    var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);



                    var weightDiary = new WeightDiary(WeightDiary.Date, WeightDiary.CurrentWeight, user);


                    //���������� � ��
                    await _context.WeightDiaries.AddAsync(weightDiary);
                    //���������� ��������� � ��
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
