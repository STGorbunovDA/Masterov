namespace Masterov.API.Models.Order;

public class UpdateOrderRequest
{
    public Guid OrderId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; } = string.Empty;

    public string? Description { get; set; }

    public Guid CustomerId { get; set; }
}