using Masterov.Domain.Extension;

namespace Masterov.Domain.Models;

public class ProductionOrderDomain
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public ProductionOrderStatus Status { get; set; }
    public List<ProductComponentDomain> Components { get; set; }
}