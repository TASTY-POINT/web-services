using TastyPoint.API.Publishing.Domain.Models;

namespace TastyPoint.API.Publishing.Domain.Repositories;

public interface IPromotionRepository
{
    Task<IEnumerable<Promotion>> ListAsync();
    Task AddAsync(Promotion promotion);
    Task<Promotion> FindByIdAsync(int id);
    Task<IEnumerable<Promotion>> FindByUserProfileIdAsync(int userProfileId);
    void Update(Promotion promotion);
    void Remove(Promotion promotion);
}