using CaclApi.DAL.Entities;
using CaclApi.Pages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace CaclApi.Pages.Meals
{
    [Authorize]
    public class IndexModel : GetConstantListPage
    {
        private readonly IMealService _mealService;
        public List<int?> sumCalorie { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchText { get; set; }

        public IndexModel(IMealService mealService)
        {
            _mealService = mealService;

        }

        public List<Meal> Meals { get; set; }

        public async Task<IActionResult> OnGetAsync(CancellationToken ct)
        {
            try
            {
                var meals = await _mealService.GetMeals(ct);
                Meals = meals;
                foreach (var meal in Meals) { meal.MealTotal(); }
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Account/Login");
            }
        }

        public async Task<IActionResult> OnPostDelete(int? id, CancellationToken ct)
        {
            var res = await _mealService.DeleteMeal(id, ct);

            if (res)
                return RedirectToPage("Index");
            else
                return NotFound();
        }

        public async Task<IActionResult> OnPostExportToExcel(CancellationToken ct)
        {
            try
            {
                var meals = await _mealService.GetMeals(ct);
                Meals = meals;
                foreach (var meal in Meals) { meal.MealTotal(); }

                // Создание нового пакета Excel
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Блюда");

                    // Заголовки столбцов
                    worksheet.Cells[1, 1].Value = "Название";
                    worksheet.Cells[1, 2].Value = "Категория блюда";
                    worksheet.Cells[1, 3].Value = "Продукты";
                    worksheet.Cells[1, 4].Value = "Калорийность";

                    // Заполнение данными
                    int row = 2;
                    foreach (var meal in Meals)
                    {
                        worksheet.Cells[row, 1].Value = meal.Name;
                        worksheet.Cells[row, 2].Value = meal.MealCategory.Name;

                        var products = meal.Ingredients.Select(i => i.Product.Name);
                        worksheet.Cells[row, 3].Value = string.Join(", ", products);

                        worksheet.Cells[row, 4].Value = meal.TotalCalories;

                        row++;
                    }

                    // Установка ширины столбцов
                    worksheet.Column(1).Width = 20;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 30;
                    worksheet.Column(4).Width = 15;

                    // Форматирование заголовков и данных
                    using (var range = worksheet.Cells[1, 1, 1, 4])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    }

                    // Сохранение пакета Excel в поток
                    MemoryStream stream = new MemoryStream();
                    package.SaveAs(stream);
                    stream.Position = 0;

                    // Отправка файла Excel в ответе
                    var fileName = "Meals.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
            catch (Exception)
            {
                return RedirectToPage("/Account/Login");
            }
        }
    }
}
