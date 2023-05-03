using System.Text.Json.Serialization;

namespace TastyPoint.API.Social.Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int Rate { get; set; }
    
    //Relationships
    public int FoodStoreId { get; set; }
    
    [JsonIgnore]
    public FoodStore FoodStore { get; set; }
}