using Masterov.API.Models.Order;

namespace Masterov.API.Models.FinishedProduct;

public class FinishedProductResponse
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
    public OrderResponse[] Orders { get; set; }
}