namespace CaclApi.DAL.Entities
{
    public class WeightDiary
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public double CurrentWeight { get; set; }
        public int? UserId { get; set; }      // внешний ключ
        public User? User { get; set; }    // навигационное свойство
    }
}
