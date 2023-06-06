using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CaclApi.DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using CaclApi.DAL.Entities;
using CaclApi.Pages.Services;
using Microsoft.EntityFrameworkCore.Internal;
using OfficeOpenXml;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();

builder.Services
    .AddTransient<IFoodIntakeService, FoodIntakeService>()
    .AddTransient<IMealService, MealService>()
    .AddTransient<IIngredientService, IngredientService>()
    .AddTransient<IProductService, ProductService>()
    .AddTransient<IWeightDiariesService, WeightDiariesService>();


builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CalcApiContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
    //options.UseSqlite("Filename=MyDatabase.db");
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.SlidingExpiration = true;
        });

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 0;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<CalcApiContext>()
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;


    var db = serviceProvider.GetRequiredService<CalcApiContext>();
    await db.Database.EnsureDeletedAsync();
    await db.Database.EnsureCreatedAsync();
    await CalcApiContextSeed.InitializeDb(db);


}
app.Run();
