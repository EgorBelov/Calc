using CaclApi.DAL.Entities;
using CaclApi.DAL;
using Microsoft.EntityFrameworkCore;

namespace CaclApi.Pages.Services
{
    public interface IProductCategoryService
    {
        Task<List<ProductCategory>> GetProductCategoriesAsync();
        Task<ProductCategory> GetProductCategoryByIdAsync(int id);
        Task CreateProductCategoryAsync(ProductCategory productCategory);
        Task UpdateProductCategoryAsync(ProductCategory productCategory);
        Task DeleteProductCategoryAsync(int id);
    }

    public class ProductCategoryService : IProductCategoryService
    {
        private readonly CalcApiContext _context;

        public ProductCategoryService(CalcApiContext context)
        {
            _context = context;
        }

        public async Task<List<ProductCategory>> GetProductCategoriesAsync()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory> GetProductCategoryByIdAsync(int id)
        {
            return await _context.ProductCategories.FindAsync(id);
        }

        public async Task CreateProductCategoryAsync(ProductCategory productCategory)
        {
            _context.ProductCategories.Add(productCategory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductCategoryAsync(ProductCategory productCategory)
        {
            _context.ProductCategories.Update(productCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductCategoryAsync(int id)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory == null)
                throw new Exception("Product category not found");

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();
        }
    }
}
