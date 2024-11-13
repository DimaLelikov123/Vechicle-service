using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicle_service.Models
{
  public class Car
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // тут ти пишеш що айдішка генерується базою
    public int Id { get; set; }

    [Required] public string Name { get; set; } = null!; // [required] вимагає це поле бути заповненим

    [Required] public string Model { get; set; } = null!;
  }
}