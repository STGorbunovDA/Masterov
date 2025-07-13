namespace Masterov.API.Models.ProductionOrder;

public class AddProductionOrderRequest
{
    public Guid FinishedProductId { get; set; }
    
    public string? Description { get; set; }
    
    public Guid? CustomerId { get; set; }
    
    public string? CustomerName { get; set; }
    
    public string? CustomerEmail { get; set; }
    
    public string? CustomerPhone { get; set; }
}