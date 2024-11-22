using System.ComponentModel.DataAnnotations;

namespace Vehicle_service.Dto.Orders
{
    public class OrderUpdateDto
    {
        [Key] public int Id { get; set; }
    
        public float Price { get; set; }

        public int CarId { get; set; }
    }
}