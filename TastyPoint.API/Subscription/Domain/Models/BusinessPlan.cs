namespace TastyPoint.API.Subscription.Domain.Models;

public class BusinessPlan
{
    public int Id { get; set; }
    public string CurrentPlan { get; set; }
    public string Description { get; set; }
    public double PlanPrice { get; set; }
    
}