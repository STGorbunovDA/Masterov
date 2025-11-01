namespace Masterov.API.Models.Supplier;

public class UpdateSupplierRequest
{
    public string Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}