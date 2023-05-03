using TastyPoint.API.Social.Domain.Models;

namespace TastyPoint.API.Social.Resources;

public class CommentResource
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int Rate { get; set; }
    public FoodStore FoodStore { get; set; }
}