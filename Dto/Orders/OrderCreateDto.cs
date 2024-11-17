using System.ComponentModel.DataAnnotations;

namespace Vehicle_service.Dto.Orders
{
    public class OrderCreateDto
    {
        [Required] public string Price { get; set; } = null!;

        [Required] public string CarId { get; set; }
    }
}