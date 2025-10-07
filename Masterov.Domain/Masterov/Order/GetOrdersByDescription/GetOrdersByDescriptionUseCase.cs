using FluentValidation;
using Masterov.Domain.Masterov.Order.GetOrdersByDescription.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrdersByDescription;

public class GetOrdersByDescriptionUseCase(IValidator<GetOrdersByDescriptionQuery> validator,
    IGetOrdersByDescriptionStorage storage) 
    : IGetOrdersByDescriptionUseCase
{
    public async Task<IEnumerable<OrderDomain>?> Execute(GetOrdersByDescriptionQuery getOrdersByDescriptionQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getOrdersByDescriptionQuery, cancellationToken);
        
        return await storage.GetOrdersByDescription(getOrdersByDescriptionQuery.Description, cancellationToken);
    }
}