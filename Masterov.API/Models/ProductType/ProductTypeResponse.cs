namespace Masterov.API.Models.ProductType;

public class ProductTypeResponse
{
    public Guid ProductTypeId { get; set; }

    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt  { get; set; }
}