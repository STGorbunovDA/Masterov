namespace Masterov.Web.Models.FinishedProduct;

public class FinishedProductDto
{
    public Guid FinishedProductId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }
    public string CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
    public int Width { get; set; }  // в мм
    public int Height { get; set; }  // в мм
    public int Depth { get; set; }  // в мм
    public byte[]? Image { get; set; }
}