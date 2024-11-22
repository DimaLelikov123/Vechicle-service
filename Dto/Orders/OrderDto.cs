using System.ComponentModel.DataAnnotations;

namespace Vehicle_service.Dto.Orders
{
    public class OrderDto
    {
        [Required] public int Id { get; set; }
    
        [Required] public float Price { get; set; }

        [Required] public int CarId { get; set; }
    }
}