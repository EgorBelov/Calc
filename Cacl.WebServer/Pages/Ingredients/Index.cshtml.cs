using CaclApi.DAL.Entities;
using CaclApi.Pages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;

namespace CaclApi.Pages.Ingredients
{
    [Authorize]
    public class IndexModel : GetConstantListPage
    {
        private readonly IIngredientService _ingredientService;

        public IndexModel(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public List<Ingredient> Ingredients { get; set; }

        public async Task<IActionResult> OnGetAsync(CancellationToken ct)
        {
            try
            {
                var ingredient = await _ingredientService.GetIngredients(ct);
                Ingredients = ingredient;

                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Account/Login");
            }
        }

        public async Task<IActionResult> OnPostDelete(int? id, CancellationToken ct)
        {
            var res = await _ingredientService.DeleteIngredient(id, ct);

            if (res)
                return RedirectToPage("Index");
            else
                return NotFound();
        }

        public async Task<IActionResult> OnPostExportToExcel(CancellationToken ct)
        {
           
                var ingredients = await _ingredientService.GetIngredients(ct);
                Ingredients = ingredients;

                // �������� ������ ������ Excel
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("�����������");

                    // ��������� ��������
                    worksheet.Cells[1, 1].Value = "�������";
                    worksheet.Cells[1, 2].Value = "��� ��������";
                    worksheet.Cells[1, 3].Value = "������������";
                    worksheet.Cells[1, 4].Value = "��������";

                    // ���������� �������
                    int row = 2;
                    foreach (var ingredient in Ingredients)
                    {
                        worksheet.Cells[row, 1].Value = ingredient.Product.Name;
                        worksheet.Cells[row, 2].Value = ingredient.ProductQuantity;
                        worksheet.Cells[row, 3].Value = ingredient.Product.Calories * ingredient.ProductQuantity;
                        worksheet.Cells[row, 4].Value = ingredient.Description;

                        row++;
                    }

                    // ��������� ������ ��������
                    worksheet.Column(1).Width = 20;
                    worksheet.Column(2).Width = 15;
                    worksheet.Column(3).Width = 15;
                    worksheet.Column(4).Width = 30;

                    // �������������� ���������� � ������
                    using (var range = worksheet.Cells[1, 1, 1, 4])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    }

                    // ���������� ������ Excel � �����
                    MemoryStream stream = new MemoryStream();
                    package.SaveAs(stream);
                    stream.Position = 0;

                    // �������� ����� Excel � ������
                    var fileName = "Ingredients.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
          
        }
    }
}
