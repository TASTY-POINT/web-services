using System.ComponentModel.DataAnnotations;

namespace TastyPoint.API.Selling.Resources;

public class SavePackResource
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    public float Price { get; set; }
    
    [Required]
    public int UserProfileId { get; set; }
}