using System.ComponentModel.DataAnnotations;

namespace Vehicle_service.Dto.Orders
{
    public class OrderDto
    {
        [Required] public int Id { get; set; }
    
        [Required] public string Price { get; set; } = null!;

        [Required] public string CarId { get; set; }
    }
}