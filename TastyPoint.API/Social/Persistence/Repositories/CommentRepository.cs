using Microsoft.EntityFrameworkCore;
using TastyPoint.API.Shared.Persistence.Contexts;
using TastyPoint.API.Shared.Persistence.Repositories;
using TastyPoint.API.Social.Domain.Models;
using TastyPoint.API.Social.Domain.Repositories;

namespace TastyPoint.API.Social.Persistence.Repositories;

public class CommentRepository: BaseRepository, ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Comment>> ListAsync()
    {
        return await _context.Comments
            .Include(p => p.FoodStore)
            .ToListAsync();
    }

    public async Task AddAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
    }

    public async Task<Comment> FindByIdAsync(int commentId)
    {
        return await _context.Comments
            .Include(p => p.FoodStore)
            .FirstOrDefaultAsync(p => p.Id == commentId);
    }

    public async Task<IEnumerable<Comment>> FindByFoodStoreAsync(int foodStoreId)
    {
        return await _context.Comments
            .Where(p => p.FoodStoreId == foodStoreId)
            .Include(p => p.FoodStore)
            .ToListAsync();
    }

    public void Update(Comment comment)
    {
        _context.Comments.Update(comment);
    }

    public void Remove(Comment comment)
    {
        _context.Comments.Remove(comment);
    }
}