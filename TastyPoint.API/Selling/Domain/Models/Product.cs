namespace TastyPoint.API.Selling.Domain.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }

    //Relationships
    public int PackId { get; set; }
    public Pack Pack { get; set; }
}