using TastyPoint.API.Publishing.Domain.Models;
using TastyPoint.API.Publishing.Domain.Services.Communication;

namespace TastyPoint.API.Publishing.Domain.Services;

public interface IPromotionService
{
    Task<IEnumerable<Promotion>> ListAsync();
    Task<PromotionResponse> FindByIdAsync(int promotionId);
    Task<IEnumerable<Promotion>> ListByUserProfileIdAsync(int userProfileId);
    Task<PromotionResponse> SaveAsync(Promotion promotion);
    Task<PromotionResponse> UpdateAsync(int promotionId, Promotion promotion);
    Task<PromotionResponse> DeleteAsync(int id);
}