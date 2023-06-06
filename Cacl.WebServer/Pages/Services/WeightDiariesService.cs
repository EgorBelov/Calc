using CaclApi.DAL;
using CaclApi.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CaclApi.Pages.Services
{

    public interface IWeightDiariesService
    {

        Task<List<WeightDiary>> GetWeightDiaries(CancellationToken ct);
        Task<WeightDiary> GetWeightDiary(int id, CancellationToken ct);
        Task<bool> DeleteFoodIntake(int? id, CancellationToken ct);
    }


    public class WeightDiariesService : IWeightDiariesService
    {
        private readonly CalcApiContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public WeightDiariesService(CalcApiContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }




        public async Task<List<WeightDiary>> GetWeightDiaries(CancellationToken ct)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
                throw new ArgumentException("Доступ закрыт");

            var weightDiaries = await _context.WeightDiaries
                .Where(x => x.UserId == user.Id)
                .ToListAsync();


            return weightDiaries;
        }




        public async Task<WeightDiary> GetWeightDiary(int id, CancellationToken ct)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
                throw new ArgumentException("Доступ закрыт");

            var weightDiary = await _context.WeightDiaries
                .Where(x => x.UserId == user.Id)
                .FirstOrDefaultAsync(x => x.Id == id);


            return weightDiary;

        }

        public async Task<bool> DeleteFoodIntake(int? id, CancellationToken ct)
        {
            if (id == null)
                return false;

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var weightDiary = await _context.WeightDiaries
               .Where(x => x.UserId == user.Id)
               .FirstOrDefaultAsync(x => x.Id == id);

            if (weightDiary == null)
                return false;

            _context.WeightDiaries.Remove(weightDiary);

            await _context.SaveChangesAsync();

            return true;
        }



    }
}
