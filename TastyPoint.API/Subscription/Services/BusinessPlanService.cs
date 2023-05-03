using TastyPoint.API.Shared.Domain.Repositories;
using TastyPoint.API.Subscription.Domain.Models;
using TastyPoint.API.Subscription.Domain.Repositories;
using TastyPoint.API.Subscription.Domain.Services;
using TastyPoint.API.Subscription.Domain.Services.Communication;

namespace TastyPoint.API.Subscription.Services;

public class BusinessPlanService : IBusinessPlanService
{
    private readonly IBusinessPlanRepository _businessPlanRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BusinessPlanService(IBusinessPlanRepository businessPlanRepository, IUnitOfWork unitOfWork)
    {
        _businessPlanRepository = businessPlanRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<BusinessPlan>> ListAsync()
    {
        return await _businessPlanRepository.ListAsync();
    }

    public async Task<BusinessPlanResponse> FindByIdAsync(int businessPlanId)
    {
        var existingBusinessPlan = await _businessPlanRepository.FindByIdAsync(businessPlanId);
        
        if (existingBusinessPlan == null)
            return new BusinessPlanResponse("Business plan not found");

        try
        {
            await _unitOfWork.CompleteAsync();
            return new BusinessPlanResponse(existingBusinessPlan);
        }
        catch (Exception e)
        {
            return new BusinessPlanResponse($"An error occurred while searching the Business Plan: {e.Message}");
        }
    }

    public async Task<BusinessPlanResponse> SaveAsync(BusinessPlan businessPlan)
    {
        try
        {
            await _businessPlanRepository.AddAsync(businessPlan);
            await _unitOfWork.CompleteAsync();
            return new BusinessPlanResponse(businessPlan);
        }
        catch (Exception e)
        {
            return new BusinessPlanResponse($"An error occurred while saving the Business Plan: {e.Message}");
        }
    }

    public async Task<BusinessPlanResponse> UpdateAsync(int businessPlanId, BusinessPlan businessPlan)
    {
        var existingBusinessPlan = await _businessPlanRepository.FindByIdAsync(businessPlanId);
        
        if (existingBusinessPlan == null)
            return new BusinessPlanResponse("Business plan not found");

        existingBusinessPlan.CurrentPlan = businessPlan.CurrentPlan;
        existingBusinessPlan.Description = businessPlan.Description;
        existingBusinessPlan.PlanPrice = businessPlan.PlanPrice;
        
        try
        {
            _businessPlanRepository.Update(existingBusinessPlan);
            await _unitOfWork.CompleteAsync();
            return new BusinessPlanResponse(existingBusinessPlan);
        }
        catch (Exception e)
        {
            return new BusinessPlanResponse($"An error occurred while updating the Business Plan: {e.Message}");
        }
    }

    public async Task<BusinessPlanResponse> DeleteAsync(int id)
    {
        var existingBusinessPlan = await _businessPlanRepository.FindByIdAsync(id);
        
        if (existingBusinessPlan == null)
            return new BusinessPlanResponse("Business plan not found");

        try
        {
            _businessPlanRepository.Remove(existingBusinessPlan);
            await _unitOfWork.CompleteAsync();
            return new BusinessPlanResponse(existingBusinessPlan);
        }
        catch (Exception e)
        {
            return new BusinessPlanResponse($"An error occurred while updating the Business Plan: {e.Message}");
        }
    }
}