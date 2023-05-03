using TastyPoint.API.Selling.Domain.Models;

namespace TastyPoint.API.Selling.Domain.Repositories;

public interface IPackRepository
{
    Task<IEnumerable<Pack>> ListAsync();
    Task<IEnumerable<Pack>> FindByUserProfileIdAsync(int userProfileId);
    Task AddAsync(Pack pack);
    Task<Pack> FindByIdAsync(int id);
    void Update(Pack pack);
    void Remove(Pack pack);
}