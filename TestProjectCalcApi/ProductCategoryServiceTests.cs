using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CaclApi.DAL.Entities;
using CaclApi.DAL;
using CaclApi.Pages.Services;
using Microsoft.EntityFrameworkCore;

namespace TestProjectCalcApi
{
    public class ProductCategoryServiceTests
    {
        private CalcApiContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<CalcApiContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            return new CalcApiContext(options);
        }

        [Fact]
        public async Task TestProductCategoryServiceCRUD()
        {
            using (var context = CreateDbContext())
            {
                var productCategoryService = new ProductCategoryService(context);

                // Create a product category
                var productCategory = new ProductCategory
                {
                    Name = "Test Category",
                    Description = "Test Description"
                };
                await productCategoryService.CreateProductCategoryAsync(productCategory);

                // Get all product categories
                var productCategories = await productCategoryService.GetProductCategoriesAsync();
                Assert.Single(productCategories);

                // Get the created product category by ID
                var createdProductCategory = await productCategoryService.GetProductCategoryByIdAsync(productCategory.Id);
                Assert.NotNull(createdProductCategory);
                Assert.Equal("Test Category", createdProductCategory.Name);

                // Update the product category
                createdProductCategory.Name = "Updated Category";
                await productCategoryService.UpdateProductCategoryAsync(createdProductCategory);

                // Verify the updated product category
                var updatedProductCategory = await productCategoryService.GetProductCategoryByIdAsync(productCategory.Id);
                Assert.Equal("Updated Category", updatedProductCategory.Name);

                // Delete the product category
                await productCategoryService.DeleteProductCategoryAsync(productCategory.Id);

                // Verify that the product category has been deleted
                var deletedProductCategory = await productCategoryService.GetProductCategoryByIdAsync(productCategory.Id);
                Assert.Null(deletedProductCategory);
            }
        }
    }
}
