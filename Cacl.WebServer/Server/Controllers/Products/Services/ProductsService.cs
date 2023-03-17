using CaclApi.DAL;
using CaclApi.DAL.Entities;
using CaclApi.Server.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CaclApi.Server.Controllers.Products.Services
{
    public interface IProductsService
    {
        Task<ProductDto> Get(int id, CancellationToken ct);
    }

    public class ProductsService : IProductsService
    { 
        private readonly CalcApiContext _context;

        public ProductsService(CalcApiContext context)
        {
            _context = context;
        }

        public async Task<ProductDto> Get(int id, CancellationToken ct)
        {
            var product = await _context.Products
                .Include(x => x.ProductCategory)
                .Include(x => x.MealCompositions)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
                throw new ArgumentException("не найден"); // todo сделать кастомную ошибку ObjectNotFoundException

            return MapToDto(product);
        }

        public ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Name = product.Name,
                ProductCategory = product?.ProductCategory?.Name,
                Calories = product.Calories,
            };
        }
    }
}
