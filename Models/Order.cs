using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicle_service.Models
{
  public class Order
  {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] public float Price { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }
        
    }
}