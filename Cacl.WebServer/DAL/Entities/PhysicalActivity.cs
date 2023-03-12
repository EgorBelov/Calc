namespace CaclApi.DAL.Entities
{
    public class PhysicalActivity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Ratio { get; set; }  
        public List<User> Users { get; set; }
    }
}
