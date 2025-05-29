namespace Masterov.API.Models.ProductType;

public class ProductTypeRequest
{
    public Guid ProductTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}