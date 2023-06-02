using CaclApi.DAL;
using CaclApi.DAL.Entities;
using CaclApi.Pages.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestProjectCalcApi
{
    public class ProductServiceTests
    {
        private CalcApiContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<CalcApiContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            return new CalcApiContext(options);
        }

        [Fact]
        public async Task TestProductServiceCRUD()
        {
            using (var context = CreateDbContext())
            {
                var productService = new MyProductService(context);

                // Create a product
                var product = new Product
                {
                    Name = "Test Product",
                    Calories = 100,
                    ProductCategoryId = 1
                };
                await productService.CreateProductAsync(product);

                // Get all products
                var products = await productService.GetProductsAsync();
                Assert.Single(products);

                // Get the created product by ID
                var createdProduct = await productService.GetProductByIdAsync(product.Id);
                Assert.NotNull(createdProduct);
                Assert.Equal("Test Product", createdProduct.Name);

                // Update the product
                createdProduct.Name = "Updated Product";
                await productService.UpdateProductAsync(createdProduct);

                // Verify the updated product
                var updatedProduct = await productService.GetProductByIdAsync(product.Id);
                Assert.Equal("Updated Product", updatedProduct.Name);

                // Delete the product
                await productService.DeleteProductAsync(product.Id);

                // Verify that the product has been deleted
                var deletedProduct = await productService.GetProductByIdAsync(product.Id);
                Assert.Null(deletedProduct);
            }
        }
    }
}