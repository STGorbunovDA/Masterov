using Masterov.API.Models.Order;

namespace Masterov.API.Models.Customer;

public class CustomerNewResponse
{
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public OrderNoCustomerNoUsedComponentsNoPaymentsRequest[] Orders { get; set; }
}