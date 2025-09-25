using Masterov.API.Models;
using Masterov.API.Models.Payment;
using Masterov.API.Models.ProductComponent;
using Masterov.Domain.Models;

namespace Masterov.API.Extensions;

public static class MapToHelper
{
    
    public static List<ProductComponentDomain>? MapToProductComponentDomain(List<ProductComponentRequest>? components)
    {
        return components?.Select(c => new ProductComponentDomain
        {
            ProductComponentId = c.ProductComponentId,
            ProductType = new ProductTypeDomain
            {
                ProductTypeId = c.ProductType.ProductTypeId,
                Name = c.ProductType.Name,
                Description = c.ProductType.Description
            },
            Warehouse = new WarehouseDomain
            {
                WarehouseId = c.WarehouseNew.WarehouseId,
                Name = c.WarehouseNew.Name
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
            PaymentDate = p.PaymentDate
        }).ToList();
    }
    
}