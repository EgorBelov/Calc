using CaclApi.DAL;
using CaclApi.DAL.Entities;
using CaclApi.Pages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


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

        [BindProperty]
        public List<SelectListItem> MealList { get; set; }

        [BindProperty]
        public List<int> SelectedMealIds { get; set; }

        public CreateModel(CalcApiContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            FoodIntakeTypeDropDownList(_context);

            var meals = await _context.Meals.ToListAsync();
            MealList = meals.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //Проверка корректности представления страницы и введенных данных
            if (ModelState.IsValid)
            {
                try
                {
                    // Получение информации о текущем авторизованном пользователе
                    var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

                    //Новый Объект класса FoodIntake и добавление ему текущего пользователя
                    //также благодаря BindProperty ему присвоены введенные данные
                    FoodIntake.User = user;

                    //Получение списка выбранных для добавления блюд
                    var selectedMeals = await _context.Meals
                    .Where(m => SelectedMealIds.Contains(m.Id))
                    .ToListAsync();

                    //Добавление блюд к приему пищи
                    FoodIntake.Meals = selectedMeals;
                    FoodIntake.FoodIntakeTotal();
                    //Добавление в бд
                    await _context.FoodIntakes.AddAsync(FoodIntake);
                    //Сохранение изменений в бд
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