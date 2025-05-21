namespace Masterov.Domain.Models;

public class ProductDomain
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UploadedAt { get; set; }
    public int Width { get; set; }  // в мм
    public int Height { get; set; }  // в мм
    public int Depth { get; set; }  // в мм
    public byte[]? Content { get; set; }
}