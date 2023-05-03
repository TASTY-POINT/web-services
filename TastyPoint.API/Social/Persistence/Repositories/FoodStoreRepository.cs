using Microsoft.EntityFrameworkCore;
using TastyPoint.API.Shared.Persistence.Contexts;
using TastyPoint.API.Shared.Persistence.Repositories;
using TastyPoint.API.Social.Domain.Models;
using TastyPoint.API.Social.Domain.Repositories;

namespace TastyPoint.API.Social.Persistence.Repositories;

public class FoodStoreRepository: BaseRepository, IFoodStoreRepository
{
    public FoodStoreRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<FoodStore>> ListAsync()
    {
        return await _context.FoodStores
            .Include(p=>p.UserProfile)
            .ToListAsync();
    }

    public async Task AddAsync(FoodStore foodStore)
    {
        await _context.FoodStores.AddAsync(foodStore);
    }

    public async Task<FoodStore> FindByIdAsync(int id)
    {
        return await _context.FoodStores
            .Include(p => p.UserProfile)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<FoodStore> FindByUserProfileId(int userProfileId)
    {
        return await _context.FoodStores
            .Include(p => p.UserProfile)
            .FirstOrDefaultAsync(p => p.UserProfileId == userProfileId);
    }

    public void Update(FoodStore foodStore)
    {
        _context.FoodStores.Update(foodStore);
    }

    public void Remove(FoodStore foodStore)
    {
        _context.FoodStores.Remove(foodStore);
    }
}