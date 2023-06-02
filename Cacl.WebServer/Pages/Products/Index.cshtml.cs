using CaclApi.DAL.Entities;
using CaclApi.Pages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;

namespace CaclApi.Pages.Products
{
    [Authorize]
    public class IndexModel : GetConstantListPage
    {
        private readonly IProductService _ingredientService;

        public IndexModel(IProductService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public List<Product> Ingredients { get; set; }

        public async Task<IActionResult> OnGetAsync(CancellationToken ct)
        {
            try
            {
                var ingredient = await _ingredientService.GetProducts(ct);
                Ingredients = ingredient;

                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Account/Login");
            }
        }

        public async Task<IActionResult> OnPostExportToExcel(CancellationToken ct)
        {
            try
            {
                var products = await _ingredientService.GetProducts(ct);
                Ingredients = products;

                // �������� ������ ������ Excel
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("��������");

                    // ��������� ��������
                    worksheet.Cells[1, 1].Value = "��������";
                    worksheet.Cells[1, 2].Value = "������������";
                    worksheet.Cells[1, 3].Value = "���������";

                    // ���������� �������
                    int row = 2;
                    foreach (var product in Ingredients)
                    {
                        worksheet.Cells[row, 1].Value = product.Name;
                        worksheet.Cells[row, 2].Value = product.Calories;
                        worksheet.Cells[row, 3].Value = product.ProductCategory.Name;

                        row++;
                    }

                    // ��������� ������ ��������
                    worksheet.Column(1).Width = 20;
                    worksheet.Column(2).Width = 15;
                    worksheet.Column(3).Width = 15;

                    // �������������� ���������� � ������
                    using (var range = worksheet.Cells[1, 1, 1, 3])
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
                    var fileName = "Products.xlsx";
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
