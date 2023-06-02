using CaclApi.DAL;
using CaclApi.DAL.Entities;
using CaclApi.Pages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace CaclApi.Pages
{
    [Authorize]
    public class IndexModel : GetConstantListPage
    {
        private readonly IFoodIntakeService _foodIntakesService;

        public IndexModel(IFoodIntakeService foodIntakesService)
        {
            _foodIntakesService = foodIntakesService;
        }
        public List<FoodIntake> FoodIntakes { get; set; }
        //public List<Ingredient> Ingredient { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchText { get; set; }
        public async Task<IActionResult> OnGetAsync(CancellationToken ct)
        {
            try
            {
                var foodIntakes = await _foodIntakesService.GetFoodIntakes(ct);
                FoodIntakes = foodIntakes;
                foreach (var food in FoodIntakes)
                {
                    foreach (var meal in food.Meals) meal.MealTotal();
                    
                    food.FoodIntakeTotal();
                }
                // Применение фильтрации
                if (!string.IsNullOrEmpty(SearchText))
                {
                    foodIntakes = foodIntakes.Where(f => f.FoodIntakeType.Name.ToLower().Contains(SearchText) ||
                                                         f.Date.ToString().ToLower().Contains(SearchText) ||
                                                         f.Meals.Any(m => m.Name.ToLower().Contains(SearchText)) ||
                                                         f.TotalCalories.ToString().ToLower().Contains(SearchText))
                                             .ToList();
                }
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Account/Login");
            }
        }

        public async Task<IActionResult> OnPostDelete(int? id, CancellationToken ct)
        {
            var res = await _foodIntakesService.DeleteFoodIntake(id, ct);

            if (res)
                return RedirectToPage("Index");
            else
                return NotFound();
        }
        public IActionResult OnPostExportToExcel(CancellationToken ct)
        {
            try
            {
                var allFoodIntakes = _foodIntakesService.GetFoodIntakes(ct).Result;

                // Применение фильтрации на основе поисковой строки
                var filteredFoodIntakes = allFoodIntakes;
                if (!string.IsNullOrEmpty(SearchText))
                {
                    filteredFoodIntakes = filteredFoodIntakes.Where(f =>
                        f.FoodIntakeType.Name.ToLower().Contains(SearchText) ||
                        f.Date.ToString().ToLower().Contains(SearchText) ||
                        f.Meals.Any(m => m.Name.ToLower().Contains(SearchText)) ||
                        f.TotalCalories.ToString().ToLower().Contains(SearchText)
                    ).ToList();
                }

                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Food Intakes");
                    worksheet.Cells[1, 1].Value = "Тип приема пищи";
                    worksheet.Cells[1, 2].Value = "Дата";
                    worksheet.Cells[1, 3].Value = "Блюда";
                    worksheet.Cells[1, 4].Value = "Калорийность";

                    int row = 2;
                    foreach (var foodIntake in filteredFoodIntakes)
                    {
                        worksheet.Cells[row, 1].Value = foodIntake.FoodIntakeType.Name;
                        worksheet.Cells[row, 2].Value = foodIntake.Date.ToString();
                        worksheet.Cells[row, 3].Value = string.Join(", ", foodIntake.Meals.Select(m => m.Name));
                        worksheet.Cells[row, 4].Value = foodIntake.TotalCalories;

                        row++;
                    }

                    // Настройка стиля заголовков
                    using (var range = worksheet.Cells[1, 1, 1, 4])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    }

                    package.Save();
                }

                stream.Position = 0;
                string fileName = "FoodIntakes.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception)
            {
                return RedirectToPage("/Account/Login");
            }
        }








    }
}
