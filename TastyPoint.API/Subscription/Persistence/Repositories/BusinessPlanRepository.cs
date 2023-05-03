using Microsoft.EntityFrameworkCore;
using TastyPoint.API.Shared.Persistence.Contexts;
using TastyPoint.API.Shared.Persistence.Repositories;
using TastyPoint.API.Subscription.Domain.Models;
using TastyPoint.API.Subscription.Domain.Repositories;

namespace TastyPoint.API.Subscription.Persistence.Repositories;

public class BusinessPlanRepository : BaseRepository, IBusinessPlanRepository
{
    public BusinessPlanRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<BusinessPlan>> ListAsync()
    {
        return await _context.BusinessPlans.ToListAsync();
    }

    public async Task AddAsync(BusinessPlan businessPlan)
    {
        await _context.BusinessPlans.AddAsync(businessPlan);
    }

    public async Task<BusinessPlan> FindByIdAsync(int id)
    {
        return await _context.BusinessPlans.FindAsync(id);
    }

    public void Update(BusinessPlan businessPlan)
    {
        _context.BusinessPlans.Update(businessPlan);
    }

    public void Remove(BusinessPlan businessPlan)
    {
        _context.BusinessPlans.Remove(businessPlan);
    }
}