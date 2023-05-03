using TastyPoint.API.Profiles.Resources;

namespace TastyPoint.API.Ordering.Resources;

public class OrderResource
{
    public int Id { get; set; }
    public string Status { get; set; }
    public string DeliveryMethod { get; set; }
    public string PaymentMethod { get; set; }
    
    //Relationships
    public UserProfileResource UserProfile { get; set; }
}