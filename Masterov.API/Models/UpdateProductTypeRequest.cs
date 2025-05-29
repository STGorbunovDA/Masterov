namespace Masterov.API.Models;

public class UpdateProductTypeRequest
{
    public Guid ProductTypeId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}