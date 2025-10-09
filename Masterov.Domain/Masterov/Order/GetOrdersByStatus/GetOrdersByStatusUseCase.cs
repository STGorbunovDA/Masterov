using FluentValidation;
using Masterov.Domain.Masterov.Order.GetOrdersByStatus.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrdersByStatus;

public class GetOrdersByStatusUseCase(IValidator<GetOrdersByStatusQuery> validator, IGetOrdersByStatusStorage storage) : IGetOrdersByStatusUseCase
{
    public async Task<IEnumerable<OrderDomain>?> Execute(GetOrdersByStatusQuery ordersByStatusQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(ordersByStatusQuery, cancellationToken);
        return await storage.GetOrdersByStatus(ordersByStatusQuery.Status, cancellationToken);
    }
}