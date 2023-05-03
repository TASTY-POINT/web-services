using Microsoft.EntityFrameworkCore;
using TastyPoint.API.Selling.Domain.Models;
using TastyPoint.API.Selling.Domain.Repositories;
using TastyPoint.API.Shared.Persistence.Contexts;
using TastyPoint.API.Shared.Persistence.Repositories;

namespace TastyPoint.API.Selling.Persistence.Repositories;

public class ProductRepository: BaseRepository, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> ListAsync()
    {
        return await _context.Products
            .Include(p=>p.Pack)
            .ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task<Product> FindByIdAsync(int productId)
    {
        return await _context.Products
            .Include(p => p.Pack)
            .FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<Product> FindByNameAsync(string name)
    {
        return await _context.Products
            .Include(p => p.Pack)
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<IEnumerable<Product>> FindByPackIdAsync(int packId)
    {
        return await _context.Products
            .Where(p => p.PackId == packId)
            .Include(p => p.Pack)
            .ToListAsync();
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    public void Remove(Product product)
    {
        _context.Products.Remove(product);
    }
}