using TastyPoint.API.Profiles.Domain.Models;

namespace TastyPoint.API.Profiles.Domain.Repositories;

public interface IUserProfileRepository
{
    Task<IEnumerable<UserProfile>> ListAsync();
    Task AddAsync(UserProfile userProfile);
    Task<UserProfile> FindByIdAsync(int id);
    Task<UserProfile> FindByUserIdAsync(int userId);
    void Update(UserProfile userProfile);
    void Remove(UserProfile userProfile);
}