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
            
        }

    }
}
