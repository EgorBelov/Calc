using CaclApi.DAL.Entities;
using CaclApi.Pages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace CaclApi.Pages.WeightDiaries
{
    [Authorize]
    public class IndexModel : GetConstantListPage
    {
        private readonly IWeightDiariesService _weightDiariesService;
        

        public IndexModel(IWeightDiariesService weightDiariesService)
        {
            _weightDiariesService = weightDiariesService;
        }
        public List<WeightDiary> WeightDiaries { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchText { get; set; }
        public async Task<IActionResult> OnGetAsync(CancellationToken ct)
        {
            try
            {
                var weightDiaries = await _weightDiariesService.GetWeightDiaries(ct);
                WeightDiaries = weightDiaries;
              
                //// Применение фильтрации
                //if (!string.IsNullOrEmpty(SearchText))
                //{
                //    foodIntakes = foodIntakes.Where(f => f.FoodIntakeType.Name.ToLower().Contains(SearchText) ||
                //                                         f.Date.ToString().ToLower().Contains(SearchText) ||
                //                                         f.Meals.Any(m => m.Name.ToLower().Contains(SearchText)) ||
                //                                         f.TotalCalories.ToString().ToLower().Contains(SearchText))
                //                             .ToList();
                //}
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Account/Login");
            }
        }

        public async Task<IActionResult> OnPostDelete(int? id, CancellationToken ct)
        {
            var res = await _weightDiariesService.DeleteFoodIntake(id, ct);

            if (res)
                return RedirectToPage("Index");
            else
                return NotFound();
        }
        public IActionResult OnPostExportToExcel(CancellationToken ct)
        {

            var allFoodIntakes = _weightDiariesService.GetWeightDiaries(ct).Result;

     

            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Дневник веса");
                worksheet.Cells[1, 1].Value = "Дата";
                worksheet.Cells[1, 2].Value = "Текущий вес";
                worksheet.Cells[1, 3].Value = "Норма калорий";

                int row = 2;
                foreach (var foodIntake in allFoodIntakes)
                {
                    worksheet.Cells[row, 1].Value = foodIntake.Date.ToString();
                    worksheet.Cells[row, 2].Value = foodIntake.CurrentWeight;                  
                    worksheet.Cells[row, 3].Value = foodIntake.NormCalories;

                    row++;
                }

                // Настройка стиля заголовков
                using (var range = worksheet.Cells[1, 1, 1, 3])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                package.Save();
            }

            stream.Position = 0;
            string fileName = "WeightDiaries.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);


        }








    }
}
