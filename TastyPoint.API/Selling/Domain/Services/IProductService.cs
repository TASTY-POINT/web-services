using TastyPoint.API.Selling.Domain.Models;
using TastyPoint.API.Selling.Domain.Services.Communication;

namespace TastyPoint.API.Selling.Domain.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> ListAsync();
    Task<IEnumerable<Product>> ListByPackIdAsync(int packId);
    Task<ProductResponse> FindByIdAsync(int packId);
    Task<ProductResponse> SaveAsync(Product product);
    Task<ProductResponse> UpdateAsync(int productId, Product product);
    Task<ProductResponse> DeleteAsync(int productId);
}