using Masterov.API.Models.Order;

namespace Masterov.API.Models.Customer;

public class CustomerRequest
{
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public OrderNoCustomerNoComponentsRequest[] Orders { get; set; }
}