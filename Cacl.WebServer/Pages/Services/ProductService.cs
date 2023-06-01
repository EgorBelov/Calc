using CaclApi.DAL;
using CaclApi.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace CaclApi.Pages.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts(CancellationToken ct);
    }
    public class ProductService : IProductService
    {

        private readonly CalcApiContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ProductService(CalcApiContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

    


        public async Task<List<Product>> GetProducts(CancellationToken ct)
        {
            //var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            //if (user == null)
            //    throw new ArgumentException("Доступ закрыт");

            var meals = await _context.Products.Include(x => x.ProductCategory)
               
                .ToListAsync();


            return meals;
        }
    }
}
