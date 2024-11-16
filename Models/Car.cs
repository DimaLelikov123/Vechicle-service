using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicle_service.Models
{
  public class Car
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public string Name { get; set; } = null!;

    [Required] public string Model { get; set; } = null!;
  }
}