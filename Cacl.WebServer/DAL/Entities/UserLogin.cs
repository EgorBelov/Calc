namespace CaclApi.DAL.Entities
{
    public class UserLogin
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
