using Masterov.API.Models.Supply;

namespace Masterov.API.Models.Supplier;

public class SupplierResponse
{
    public Guid SupplierId { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public SupplyNewResponse[] Supplies { get; set; }
}