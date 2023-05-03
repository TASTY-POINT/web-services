using TastyPoint.API.Profiles.Resources;

namespace TastyPoint.API.Publishing.Resources;

public class PromotionResource
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public UserProfileResource UserProfile { get; set; }
}