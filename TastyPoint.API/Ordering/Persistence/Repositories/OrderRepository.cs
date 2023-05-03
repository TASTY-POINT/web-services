using Microsoft.EntityFrameworkCore;
using TastyPoint.API.Ordering.Domain.Models;
using TastyPoint.API.Ordering.Domain.Repositories;
using TastyPoint.API.Shared.Persistence.Contexts;
using TastyPoint.API.Shared.Persistence.Repositories;

namespace TastyPoint.API.Ordering.Persistence.Repositories;

public class OrderRepository: BaseRepository, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> ListAsync()
    {
        return await _context.Orders
            .Include(p=>p.UserProfile)
            .ToListAsync();
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public async Task<Order> FindByIdAsync(int id)
    {
        return await _context.Orders
            .Include(p=>p.UserProfile)
            .FirstOrDefaultAsync(p=>p.Id == id);
    }

    public async Task<IEnumerable<Order>> FindByUserProfileIdAsync(int userProfileId)
    {
        return await _context.Orders
            .Where(p => p.UserProfileId == userProfileId)
            .Include(p => p.UserProfile)
            .ToListAsync();
    }

    public void Update(Order order)
    {
        _context.Orders.Update(order);
    }

    public void Remove(Order order)
    {
        _context.Orders.Remove(order);
    }
}