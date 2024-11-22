using System.ComponentModel.DataAnnotations;

namespace Vehicle_service.Dto.Orders
{
    public class OrderCreateDto
    {
        public float Price { get; set; }

        public int CarId { get; set; }
    }
}