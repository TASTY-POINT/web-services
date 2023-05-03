namespace TastyPoint.API.Subscription.Resources;

public class BusinessPlanResource
{
    public int Id { get; set; }
    public string CurrentPlan { get; set; }
    public string Description { get; set; }
    public double PlanPrice { get; set; }
}