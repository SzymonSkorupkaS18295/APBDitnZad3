using System.ComponentModel.DataAnnotations;

namespace APBDcw3.Animals;

public class AnimalDTO
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Category { get; set; }
    [Required]
    public string Area { get; set; }
}