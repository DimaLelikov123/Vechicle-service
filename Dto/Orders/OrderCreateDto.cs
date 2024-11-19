using System.ComponentModel.DataAnnotations;

namespace Vehicle_service.Dto.Orders
{
    public class OrderCreateDto
    {
        [Required] public float Price { get; set; }

        [Required] public int CarId { get; set; }
    }
}