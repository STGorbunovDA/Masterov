namespace Masterov.API.Models.Supplier;

public class UpdateSupplierRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? CreatedAt { get; set; }
}