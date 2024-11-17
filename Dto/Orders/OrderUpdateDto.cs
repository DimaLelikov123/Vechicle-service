using System.ComponentModel.DataAnnotations;

namespace Vehicle_service.Dto.Orders
{
    public class OrderUpdateDto
    {
        [Key] public int Id { get; set; }
    
        [Required] public string Price { get; set; } = null!;

        [Required] public string CarId { get; set; }
    }
}