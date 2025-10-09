using FluentValidation;
using Masterov.Domain.Masterov.Order.GetOrdersByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrdersByUpdatedAt;

public class GetOrdersByUpdatedAtUseCase(IValidator<GetOrdersByUpdatedAtQuery> validator, IGetOrdersByUpdatedAtStorage storage) 
    : IGetOrdersByUpdatedAtUseCase
{
    public async Task<IEnumerable<OrderDomain>?> Execute(GetOrdersByUpdatedAtQuery ordersByUpdatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(ordersByUpdatedAtQuery, cancellationToken);
        
        return await storage.GetOrdersByUpdatedAt(ordersByUpdatedAtQuery.UpdatedAt, cancellationToken);
    }
}