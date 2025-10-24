using Masterov.API.Models.Order;

namespace Masterov.API.Models.Customer;

public class CustomerResponse
{
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public Guid? UserId { get; set; }
    public OrderNoCustomerNoUsedComponentsRequest[] Orders { get; set; }
}