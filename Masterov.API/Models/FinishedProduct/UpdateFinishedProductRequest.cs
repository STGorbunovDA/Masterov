namespace Masterov.API.Models.FinishedProduct;

public class UpdateFinishedProductRequest
{
    public string Name { get; set; }
    public string Type { get; set; }
    public decimal? Price { get; set; }
    public int? Width { get; set; }  // в мм
    public int? Height { get; set; }  // в мм
    public int? Depth { get; set; }  // в мм
    public IFormFile? Image { get; set; }
    public string? CreatedAt { get; set; }
}