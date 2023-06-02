using Microsoft.EntityFrameworkCore;
using CaclApi.DAL.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Principal;

namespace CaclApi.DAL
{
    public class CalcApiContext : IdentityDbContext<User>
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Meal> Meals => Set<Meal>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<User> Users => Set<User>();
        public DbSet<FoodIntake> FoodIntakes => Set<FoodIntake>();
        public DbSet<FoodIntakeType> FoodIntakeTypes=> Set<FoodIntakeType>();
        public DbSet<WeightDiary> WeightDiaries=> Set<WeightDiary>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        public DbSet<MealCategory> MealCategories => Set<MealCategory>();
        public DbSet<PhysicalActivity> PhysicalActivities => Set<PhysicalActivity>();
        public CalcApiContext(DbContextOptions<CalcApiContext> options)
            : base(options)
        {
        }
    }
}
