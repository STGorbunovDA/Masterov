namespace Masterov.API.Models.Supplier;

public class AddSupplierRequest
{
    public string Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}