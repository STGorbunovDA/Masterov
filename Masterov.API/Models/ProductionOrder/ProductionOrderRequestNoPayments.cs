using Masterov.API.Models.Customer;
using Masterov.API.Models.ProductComponent;
using Masterov.Domain.Extension;

namespace Masterov.API.Models.ProductionOrder;

public class ProductionOrderRequestNoPayments
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public ProductionOrderStatus Status { get; set; }
    public string? Description { get; set; }
    public CustomerNoOrdersRequest CustomerNoOrders { get; set; }
    public List<ProductComponentRequest> Components { get; set; }
}