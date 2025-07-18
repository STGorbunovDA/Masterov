namespace Masterov.Domain.Models;

public class ProductTypeDomain
{
    public Guid ProductTypeId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}