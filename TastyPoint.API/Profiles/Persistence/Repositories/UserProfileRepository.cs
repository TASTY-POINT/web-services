using Microsoft.EntityFrameworkCore;
using TastyPoint.API.Profiles.Domain.Models;
using TastyPoint.API.Profiles.Domain.Repositories;
using TastyPoint.API.Shared.Persistence.Contexts;
using TastyPoint.API.Shared.Persistence.Repositories;

namespace TastyPoint.API.Profiles.Persistence.Repositories;

public class UserProfileRepository: BaseRepository, IUserProfileRepository
{
    public UserProfileRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<UserProfile>> ListAsync()
    {
        return await _context.UserProfiles
            .Include(p=>p.User)
            .ToListAsync();
    }

    public async Task AddAsync(UserProfile userProfile)
    {
        await _context.UserProfiles.AddAsync(userProfile);
    }

    public async Task<UserProfile> FindByIdAsync(int id)
    {
        return await _context.UserProfiles
            .Include(p=>p.User)
            .FirstOrDefaultAsync(p=>p.Id == id);
    }

    public async Task<UserProfile> FindByUserIdAsync(int userId)
    {
        return await _context.UserProfiles
            .Include(p => p.User)
            .FirstOrDefaultAsync(p=>p.UserId == userId);
    }

    public void Update(UserProfile userProfile)
    {
        _context.UserProfiles.Update(userProfile);
    }

    public void Remove(UserProfile userProfile)
    {
        _context.UserProfiles.Remove(userProfile);
    }
}