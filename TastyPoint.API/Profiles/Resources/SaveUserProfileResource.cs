using System.ComponentModel.DataAnnotations;

namespace TastyPoint.API.Profiles.Resources;

public class SaveUserProfileResource
{
    [MaxLength(250)]
    public string Name { get; set; }
    
    public string PhoneNumber { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Type { get; set; }
    
    [Required]
    public int UserId { get; set; }
}