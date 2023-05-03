using TastyPoint.API.Ordering.Domain.Models;
using TastyPoint.API.Ordering.Domain.Services.Communication;

namespace TastyPoint.API.Ordering.Domain.Services;

public interface IOrderService
{
    Task<IEnumerable<Order>> ListAsync();
    Task<IEnumerable<Order>> ListByUserProfileIdAsync(int userProfileId);
    Task<OrderResponse> FindByIdAsync(int orderId);
    Task<OrderResponse> SaveAsync(Order order);
    Task<OrderResponse> UpdateAsync(int orderId, Order order);
    Task<OrderResponse> DeleteAsync(int id);
}