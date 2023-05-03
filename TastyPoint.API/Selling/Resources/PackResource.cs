using TastyPoint.API.Profiles.Resources;

namespace TastyPoint.API.Selling.Resources;

public class PackResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public UserProfileResource UserProfile { get; set; }
}