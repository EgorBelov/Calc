using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace CaclApi.DAL.Entities
{
    public class User : IdentityUser
    {
        //public int Id { get; set; }
        //public string Login { get; set; }
        //public string Password { get; set; }

        public string? Name { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public int? Age { get; set; }
        public string? Sex { get; set; }
        public List<FoodIntake>? FoodIntakes { get; set; } = new();
        public List<WeightDiary>? WeightDiaries { get; set; } = new();
        public int? PhysicalActivityId { get; set; }
        public PhysicalActivity? PhysicalActivity { get; set; }
        public User(string email, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            var hashedPassword = passwordHasher.HashPassword(this, password);

            base.Email = email;
            base.UserName = email;
            base.PasswordHash = hashedPassword;
        }

        public User()
        {
        }
    }
}
