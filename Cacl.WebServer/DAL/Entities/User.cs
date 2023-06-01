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
        public int PhysicalActivityId { get; set; }
        public PhysicalActivity PhysicalActivity { get; set; }
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
        public double NormCal()
        {
            double res = 0.0;
            if (this.Sex == "Male")
            {
                res = (88.36 + (13.4 * (double)this.Weight) + (4.8 * (double)this.Height) - (5.7 * (double)this.Age)) * (double)(this.PhysicalActivity?.Ratio ?? 0);
            }
            else
            {
               res = (447.6 + (9.2 * (double)this.Weight) + (3.1 * (double)this.Height) - (4.3* (double)this.Age)) * (double)(this.PhysicalActivity?.Ratio ?? 0);
            }
            return res;
        }
    }
}
