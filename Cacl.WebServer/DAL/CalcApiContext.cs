using Microsoft.EntityFrameworkCore;
using CaclApi.DAL.Entities;
using System.Collections.Generic;

namespace CaclApi.DAL
{
    public class CalcApiContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Meal> Meals => Set<Meal>();
        public DbSet<MealComposition> MealsCompositions => Set<MealComposition>();
        public DbSet<User> Users => Set<User>();
        public DbSet<FoodIntake> FoodIntakes => Set<FoodIntake>();
        public DbSet<FoodIntakeType> FoodIntakeTypes=> Set<FoodIntakeType>();
        public DbSet<WeightDiary> WeightDiaries=> Set<WeightDiary>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        public DbSet<MealCategory> MealCategories => Set<MealCategory>();
        public DbSet<UserLogin> UserLogins => Set<UserLogin>();
        public DbSet<PhysicalActivity> PhysicalActivities => Set<PhysicalActivity>();
        public CalcApiContext(DbContextOptions<CalcApiContext> options)
            : base(options)
        {
            //Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
