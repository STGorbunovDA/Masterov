using Masterov.API.Models.Payment;
using Masterov.API.Models.UsedComponent;
using Masterov.Domain.Models;

namespace Masterov.API.Extensions;

public static class MapToHelper
{
    
    public static List<UsedComponentDomain>? MapToProductComponentDomain(List<UsedComponentResponse>? components)
    {
        return components?.Select(c => new UsedComponentDomain
        {
            UsedComponentId = c.UsedComponentId,
            ComponentType = new ComponentTypeDomain
            {
                ComponentTypeId = c.ComponentType.ComponentTypeId,
                Name = c.ComponentType.Name,
                Description = c.ComponentType.Description
            },
            Warehouse = new WarehouseDomain
            {
                WarehouseId = c.Warehouse.WarehouseId,
                Name = c.Warehouse.Name
            },
            Quantity = c.Quantity
        }).ToList();
    }
    
    public static List<PaymentDomain>? MapToPaymentDomain(List<PaymentUpdateRequest>? payments)
    {
        return payments?.Select(p => new PaymentDomain
        {
            PaymentId = p.PaymentId,
            Customer = new CustomerDomain
            {
                CustomerId = p.Customer.CustomerId,
                Name = p.Customer.Name,
                Email = p.Customer.Email,
                Phone = p.Customer.Phone
            },
            MethodPayment = p.MethodPayment,
            Amount = p.Amount,
            CreatedAt = p.CreatedAt
        }).ToList();
    }
    
}