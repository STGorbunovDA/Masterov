using FluentValidation;
using Masterov.Domain.Masterov.Order.GetOrdersByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrdersByCreatedAt;

public class GetOrdersByCreatedAtUseCase(IValidator<GetOrdersByCreatedAtQuery> validator,
    IGetOrdersByCreatedAtStorage storage) 
    : IGetOrdersByCreatedAtUseCase
{
    public async Task<IEnumerable<OrderDomain>?> Execute(GetOrdersByCreatedAtQuery ordersByCreatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(ordersByCreatedAtQuery, cancellationToken);
        
        return await storage.GetOrdersByCreatedAt(ordersByCreatedAtQuery.CreatedAt, cancellationToken);
    }
}