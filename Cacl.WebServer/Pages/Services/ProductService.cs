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

    public interface IMyProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }

    public class MyProductService : IMyProductService
    {

        private readonly CalcApiContext _context;


        public MyProductService(CalcApiContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new Exception("Product not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
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
