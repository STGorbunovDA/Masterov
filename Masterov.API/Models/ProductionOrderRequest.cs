using Masterov.Domain.Extension;

namespace Masterov.API.Models;

public class ProductionOrderRequest
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public ProductionOrderStatus Status { get; set; }
    public List<ProductComponentRequest> Components { get; set; }
}