using CaclApi.DAL.Entities;
using CaclApi.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CaclApi.Pages.Services
{
    
    public class GetConstantListPage : PageModel
    {



        public SelectList MealSL { get; set; }
        public SelectList FoodIntakeTypeSL { get; set; }
        public SelectList ProductSL { get; set; }
        public SelectList IngredientSL { get; set; }
        public SelectList MealCategorySL { get; set; }

        public SelectList FoodIntakeSL { get; set; }


        public void FoodIntakeTypeDropDownList(CalcApiContext context, object value = null)
        {
            var query = context.FoodIntakeTypes.OrderBy(x => x.Name);

            FoodIntakeTypeSL = new SelectList(query.AsNoTracking(),
                        "Id", "Name", value);

            
        }

        public void MealDropDownList(CalcApiContext context, object value = null)
        {
            var query = context.Meals.OrderBy(x => x.Name);

            MealSL = new SelectList(query.AsNoTracking(),
                        "Id", "Name", value);

            //MealSL = await context.Ingredient.ToListAsync();
        }
        public void MealCategoryDropDownList(CalcApiContext context, object value = null)
        {
            var query = context.MealCategories.OrderBy(x => x.Name);

            MealCategorySL = new SelectList(query.AsNoTracking(),
                        "Id", "Name", value);


        }
        public void ProductDropDownList(CalcApiContext context, object value = null)
        {
            var query = context.Products.OrderBy(x => x.Name);

            ProductSL = new SelectList(query.AsNoTracking(),
                        "Id", "Name", value);
        }

        public void IngredientDropDownList(CalcApiContext context, object value = null)
        {
            var query = context.Ingredients.OrderBy(x => x.ProductQuantity);

            IngredientSL = new SelectList(query.AsNoTracking(),
                        "Id", "ProductQuantity", value);
        }

    }
}
