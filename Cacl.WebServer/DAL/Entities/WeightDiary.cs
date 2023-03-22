namespace CaclApi.DAL.Entities
{
    public class WeightDiary
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public double CurrentWeight { get; set; }
        public string? UserId { get; set; }      
        public User? User { get; set; }   
    }
}
