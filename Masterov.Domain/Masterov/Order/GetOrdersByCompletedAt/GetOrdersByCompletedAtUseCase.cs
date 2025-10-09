using FluentValidation;
using Masterov.Domain.Masterov.Order.GetOrdersByCompletedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrdersByCompletedAt;

public class GetOrdersByCompletedAtUseCase(IValidator<GetOrdersByCompletedAtQuery> validator,
    IGetOrdersByCompletedAtStorage storage) : IGetOrdersByCompletedAtUseCase
{
    public async Task<IEnumerable<OrderDomain>?> Execute(GetOrdersByCompletedAtQuery getOrdersByCompletedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getOrdersByCompletedAtQuery, cancellationToken);
        
        return await storage.GetOrdersByCompletedAt(getOrdersByCompletedAtQuery.CompletedAt, cancellationToken);
    }
}