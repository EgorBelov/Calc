namespace CaclApi.DAL.Entities
{
    public class WeightDiary
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double CurrentWeight { get; set; }
        public string? UserId { get; set; }      
        public User? User { get; set; }
        public int NormCalories { get; set; } = 0;

        public WeightDiary()
        {
            
        }
        public WeightDiary(DateTime date, double weight, User user)
        {
            this.Date = date;
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            this.Date.Add(currentTime);
            this.CurrentWeight = weight;
            this.User = user;
            this.NormCalories = (int)(24*weight);
        }
    }
}
