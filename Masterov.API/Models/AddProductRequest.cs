namespace Masterov.API.Models;

public class AddProductRequest
{
    public string Name { get; set; }
    public string Type { get; set; }
    public decimal? Price { get; set; }
    public int? Width { get; set; }  // в мм
    public int? Height { get; set; }  // в мм
    public int? Depth { get; set; }  // в мм
    public byte[]? Content { get; set; }
}