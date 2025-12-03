using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByTypeWithoutOrders.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByTypeWithoutOrders;

public class GetFinishedProductsByTypeWithoutOrdersUseCase(IValidator<GetFinishedProductsByTypeWithoutOrdersQuery> validator, IGetFinishedProductsByTypeWithoutOrdersStorage storage) : IGetFinishedProductsByTypeWithoutOrdersUseCase
{
    public async Task<IEnumerable<FinishedProductNoOrdersDomain?>> Execute(GetFinishedProductsByTypeWithoutOrdersQuery getFinishedProductsByTypeQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getFinishedProductsByTypeQuery, cancellationToken);
        var finishedProductExists = await storage.GetFinishedProductByTypeWithoutOrders(getFinishedProductsByTypeQuery.FinishedProductType, cancellationToken);
        
        if (finishedProductExists is null)
            throw new NotFoundByNameException(getFinishedProductsByTypeQuery.FinishedProductType, "Готовое мебельное изделие");
        
        return finishedProductExists;
    }
}