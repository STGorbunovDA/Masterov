using Masterov.API.Models.Order;

namespace Masterov.API.Models.Customer;

public class CustomerNewRequest
{
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public OrderNoCustomerNoComponentsNoPaymentsRequest[] Orders { get; set; }
}