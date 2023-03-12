namespace CaclApi.DAL.Entities
{
    public class WeightDiary
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public int CurrentWeight { get; set; }
        public Guid? UserId { get; set; }      // внешний ключ
        public User? User { get; set; }    // навигационное свойство
    }
}
