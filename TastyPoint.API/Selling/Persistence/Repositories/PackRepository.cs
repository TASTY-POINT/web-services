using Microsoft.EntityFrameworkCore;
using TastyPoint.API.Selling.Domain.Models;
using TastyPoint.API.Selling.Domain.Repositories;
using TastyPoint.API.Shared.Persistence.Contexts;
using TastyPoint.API.Shared.Persistence.Repositories;

namespace TastyPoint.API.Selling.Persistence.Repositories;

public class PackRepository: BaseRepository, IPackRepository
{
    public PackRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Pack>> ListAsync()
    {
        return await _context.Packs
            .Include(p=>p.UserProfile)
            .ToListAsync();
    }

    public async Task<IEnumerable<Pack>> FindByUserProfileIdAsync(int userProfileId)
    {
        return await _context.Packs
            .Where(p => p.UserProfileId == userProfileId)
            .Include(p => p.UserProfile)
            .ToListAsync();
    }

    public async Task AddAsync(Pack pack)
    {
        await _context.Packs.AddAsync(pack);
    }

    public async Task<Pack> FindByIdAsync(int id)
    {
        return await _context.Packs
            .Include(p=>p.UserProfile)
            .FirstOrDefaultAsync(p=>p.Id==id);
    }

    public void Update(Pack pack)
    {
        _context.Packs.Update(pack);
    }

    public void Remove(Pack pack)
    {
        _context.Packs.Remove(pack);
    }
}