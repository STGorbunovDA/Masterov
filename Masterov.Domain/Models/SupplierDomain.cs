namespace Masterov.Domain.Models;

public class SupplierDomain
{
    public Guid SupplierId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public SupplyDomain[] Supplies { get; set; }
}