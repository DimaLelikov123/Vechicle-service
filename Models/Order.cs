using System.ComponentModel.DataAnnotations;

namespace Vehicle_service.Models
{
  public class Order
  {
    [Key] public int Id { get; set; }

    [Required] public float Price { get; set; }

    public int CarId { get; set; }
  }
}