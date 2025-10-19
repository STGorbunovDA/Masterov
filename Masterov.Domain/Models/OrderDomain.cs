using Masterov.Domain.Extension;

namespace Masterov.Domain.Models;

public class OrderDomain
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt  { get; set; }
    public DateTime? CompletedAt { get; set; }
    public OrderStatus Status { get; set; }
    public string? Description { get; set; }
    public CustomerDomain Customer { get; set; }
    public IEnumerable<ComponentsDomain> Components { get; set; }
    public IEnumerable<PaymentDomain> Payments { get; set; }
    public Guid FinishedProductId { get; set; }
    public decimal FullPriceFinishedProduct { get; set; }
}