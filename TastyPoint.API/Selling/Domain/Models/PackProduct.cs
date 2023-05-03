namespace TastyPoint.API.Selling.Domain.Models;

public class PackProduct
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int PackId { get; set; }
    public Pack Pack { get; set; }
}