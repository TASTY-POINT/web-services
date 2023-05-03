using TastyPoint.API.Profiles.Domain.Models;

namespace TastyPoint.API.Profiles.Domain.Repositories;

public interface IBusinessProfileRepository
{
    Task<IEnumerable<BusinessProfile>> ListAsync();
    Task AddAsync(BusinessProfile businessProfile);
    Task<BusinessProfile> FindByIdAsync(int id);
    void Update(BusinessProfile businessProfile);
    void Remove(BusinessProfile businessProfile);
}