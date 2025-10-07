namespace Masterov.API.Models.Order;

public class UpdateOrderStatusRequest
{
    public Guid OrderId { get; set; }
    public string Status { get; set; }
}