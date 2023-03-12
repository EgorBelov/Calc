using CaclApi.DAL.Entities;

namespace CaclApi.Server.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public string ProductCategory { get; set; } 
    }
}
