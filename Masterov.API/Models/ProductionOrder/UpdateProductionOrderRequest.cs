namespace Masterov.API.Models.ProductionOrder;

public class UpdateProductionOrderRequest
{
    public Guid OrderId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; } = string.Empty;

    public string? Description { get; set; }

    public Guid CustomerId { get; set; }
}