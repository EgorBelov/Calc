namespace CaclApi.DAL.Entities
{
    public class UserLogin
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
