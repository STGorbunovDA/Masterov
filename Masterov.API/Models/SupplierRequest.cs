namespace Masterov.API.Models;

public class SupplierRequest
{
    public Guid SupplierId { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}