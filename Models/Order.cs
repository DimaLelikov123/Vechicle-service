using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicle_service.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public float Price { get; set; }

        public int CarId { get; set; }

        [ForeignKey("CarId")]
        public Car Car { get; set; } = null!;
    }
}
