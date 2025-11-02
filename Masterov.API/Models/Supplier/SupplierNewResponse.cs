namespace Masterov.API.Models.Supplier;

public class SupplierNewResponse
{
    public Guid SupplierId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt  { get; set; }
}