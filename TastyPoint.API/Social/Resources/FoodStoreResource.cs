using TastyPoint.API.Profiles.Resources;

namespace TastyPoint.API.Social.Resources;

public class FoodStoreResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public int Rate { get; set; }
    public bool Favorite { get; set; }
    public string Image { get; set; }
    public UserProfileResource UserProfile { get; set; }
}