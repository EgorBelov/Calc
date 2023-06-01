namespace CaclApi.DAL.Entities
{
    public class PhysicalActivity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double? Ratio { get; set; }  
        public List<User> Users { get; set; }
    }
}
