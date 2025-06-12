namespace Masterov.API.Models.FinishedProduct;

public class GetFinishedProductOrdersRequest
{
    public Guid FinishedProductId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? Status { get; set; }
    public string? Description { get; set; }
}