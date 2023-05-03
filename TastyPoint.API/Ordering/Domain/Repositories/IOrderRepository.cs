using TastyPoint.API.Ordering.Domain.Models;

namespace TastyPoint.API.Ordering.Domain.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> ListAsync();
    Task AddAsync(Order order);
    Task<Order> FindByIdAsync(int id);
    Task<IEnumerable<Order>> FindByUserProfileIdAsync(int userProfileId);
    void Update(Order order);
    void Remove(Order order);
}