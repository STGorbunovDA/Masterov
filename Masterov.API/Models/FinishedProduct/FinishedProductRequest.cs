using Masterov.API.Models.ProductionOrder;

namespace Masterov.API.Models.FinishedProduct;

public class FinishedProductRequest
{
    public Guid FinishedProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int Width { get; set; }  // в мм
    public int Height { get; set; }  // в мм
    public int Depth { get; set; }  // в мм
    public byte[]? Image { get; set; }
    public ProductionOrderRequest[] Orders { get; set; }
}