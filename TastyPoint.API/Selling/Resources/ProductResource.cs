namespace TastyPoint.API.Selling.Resources;

public class ProductResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public PackResource Pack { get; set; }
}