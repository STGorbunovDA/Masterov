namespace Masterov.API.Models.Order;

public class UpdateOrderRequest
{
    public string CreatedAt { get; set; }
    public string CompletedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid FinishedProductId { get; set; }
    public Guid CustomerId { get; set; }
}