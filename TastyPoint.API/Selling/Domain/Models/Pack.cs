using TastyPoint.API.Profiles.Domain.Models;

namespace TastyPoint.API.Selling.Domain.Models;

public class Pack
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    
    //Relationships
    public IList<Product> Products { get; set; } = new List<Product>();
    public int UserProfileId { get; set; }
    public UserProfile UserProfile { get; set; }
}