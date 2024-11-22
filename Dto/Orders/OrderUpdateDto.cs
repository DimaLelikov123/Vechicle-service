using System.ComponentModel.DataAnnotations;

namespace Vehicle_service.Dto.Orders
{
    public class OrderUpdateDto
    {
        [Key] public int Id { get; set; }
    
        [Required] public float Price { get; set; }

        [Required] public int CarId { get; set; }
    }
}