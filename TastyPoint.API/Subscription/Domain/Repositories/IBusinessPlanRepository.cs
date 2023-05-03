using TastyPoint.API.Subscription.Domain.Models;

namespace TastyPoint.API.Subscription.Domain.Repositories;

public interface IBusinessPlanRepository
{
    Task<IEnumerable<BusinessPlan>> ListAsync();
    Task AddAsync(BusinessPlan businessPlan);
    Task<BusinessPlan> FindByIdAsync(int id);
    void Update(BusinessPlan businessPlan);
    void Remove(BusinessPlan businessPlan);
}