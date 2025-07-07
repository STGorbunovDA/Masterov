using Masterov.Domain.Extension;

namespace Masterov.API.Models.ProductionOrder;

public class UpdateProductionOrderStatusRequest
{
    public Guid OrderId { get; set; }
    public string Status { get; set; }
}