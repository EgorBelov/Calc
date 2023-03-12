using CaclApi.DAL.Entities;

namespace CaclApi.DAL
{
    public class CalcApiContextSeed
    {
        public static async Task InitializeDb(CalcApiContext db)
        {
            var product1 = new Product() { Name = "Яблоко", Calories = 150 };
            await db.Products.AddAsync(product1);

            await db.SaveChangesAsync();
        }
    }
}
