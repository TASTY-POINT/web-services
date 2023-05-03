using System.ComponentModel.DataAnnotations;

namespace TastyPoint.API.Ordering.Resources;

public class SaveOrderResource
{
    [Required]
    public string DeliveryMethod { get; set; }
    
    [Required]
    public string PaymentMethod { get; set; }
    
    [Required]
    public string Status { get; set; }

    [Required]
    public int UserProfileId { get; set; }
}