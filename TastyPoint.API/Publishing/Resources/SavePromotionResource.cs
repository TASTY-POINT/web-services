using System.ComponentModel.DataAnnotations;
using TastyPoint.API.Selling.Domain.Models;

namespace TastyPoint.API.Publishing.Resources;

public class SavePromotionResource
{
    [Required]
    [MaxLength(300)]
    public string? Title { get; set; }
    
    [MaxLength(300)]
    public string? SubTitle { get; set; }
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    [MaxLength(500)]
    public string? Image { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
    public string? EntryDate { get; set; }
    
    public string? ExpirationDate { get; set; }
    
    [Required]
    public int UserProfileId { get; set; }
}