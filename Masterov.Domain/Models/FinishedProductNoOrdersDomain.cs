namespace Masterov.Domain.Models;

public class FinishedProductNoOrdersDomain
{
    public Guid FinishedProductId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int Width { get; set; }  // в мм
    public int Height { get; set; }  // в мм
    public int Depth { get; set; }  // в мм
    public bool Elite { get; set; }
    public byte[]? Image { get; set; }
}