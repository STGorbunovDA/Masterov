using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByName;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByName.Query;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByType.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByType;

public class GetFinishedProductsByTypeUseCase(IValidator<GetFinishedProductsByTypeQuery> validator, IGetFinishedProductByTypeStorage storage) : IGetFinishedProductsByTypeUseCase
{
    public async Task<IEnumerable<FinishedProductDomain?>> Execute(GetFinishedProductsByTypeQuery getFinishedProductsByTypeQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getFinishedProductsByTypeQuery, cancellationToken);
        var finishedProductExists = await storage.GetFinishedProductByType(getFinishedProductsByTypeQuery.FinishedProductType, cancellationToken);
        
        if (finishedProductExists is null)
            throw new NotFoundByNameException(getFinishedProductsByTypeQuery.FinishedProductType, "Готовое мебельное изделие");
        
        return finishedProductExists;
    }
}