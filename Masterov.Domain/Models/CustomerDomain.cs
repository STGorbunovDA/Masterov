namespace Masterov.Domain.Models;

public class CustomerDomain
{
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public OrderDomain[] Orders { get; set; }
}