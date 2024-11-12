using System.ComponentModel.DataAnnotations;

namespace Vehicle_service.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;
    }
}
