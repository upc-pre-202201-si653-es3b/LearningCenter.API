using System.ComponentModel.DataAnnotations;

namespace LearningCenter.API.Learning.Resources;

public class SaveTutorialResource
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [MaxLength(120)]
    public string Description { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
}