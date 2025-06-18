using Masterov.API.Models.ProductionOrder;

namespace Masterov.API.Models.Customer;

public class AddCustomerRequest
{
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}