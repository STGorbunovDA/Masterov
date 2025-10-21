namespace Masterov.API.Models.Supplier;

public class SupplierNewResponse
{
    public Guid SupplierId { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}