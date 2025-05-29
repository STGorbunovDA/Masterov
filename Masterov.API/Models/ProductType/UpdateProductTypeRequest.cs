namespace Masterov.API.Models.ProductType;

public class UpdateProductTypeRequest
{
    public Guid ProductTypeId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}