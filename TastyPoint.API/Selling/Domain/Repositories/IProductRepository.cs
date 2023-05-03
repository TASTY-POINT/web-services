using TastyPoint.API.Selling.Domain.Models;

namespace TastyPoint.API.Selling.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> ListAsync();
    Task AddAsync(Product product);
    Task<Product> FindByIdAsync(int productId);
    Task<Product> FindByNameAsync(string name);
    Task<IEnumerable<Product>> FindByPackIdAsync(int packId);
    void Update(Product product);
    void Remove(Product product);
}