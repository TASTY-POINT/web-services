using System.ComponentModel.DataAnnotations;
using TastyPoint.API.Social.Domain.Models;

namespace TastyPoint.API.Social.Resources;

public class SaveCommentResource
{
    [Required]
    [MaxLength(1000)]
    public string Text { get; set; }
    
    [Required]
    public int Rate { get; set; }
    
    [Required]
    public int FoodStoreId { get; set; }
}