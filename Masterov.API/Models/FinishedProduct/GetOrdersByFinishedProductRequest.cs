namespace Masterov.API.Models.FinishedProduct;

public class GetOrdersByFinishedProductRequest
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? Status { get; set; }
    public string? Description { get; set; }
}