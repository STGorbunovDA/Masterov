using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByName;

public class GetFinishedProductsByNameUseCase(IValidator<GetFinishedProductsByNameQuery> validator, IGetFinishedProductByNameStorage storage) : IGetFinishedProductsByNameUseCase
{
    public async Task<IEnumerable<FinishedProductDomain?>> Execute(GetFinishedProductsByNameQuery getFinishedProductsByNameQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getFinishedProductsByNameQuery, cancellationToken);
        var finishedProductExists = await storage.GetFinishedProductByName(getFinishedProductsByNameQuery.FinishedProductName, cancellationToken);
        
        if (finishedProductExists is null)
            throw new NotFoundByNameException(getFinishedProductsByNameQuery.FinishedProductName, "Готовое мебельное изделие");
        
        return finishedProductExists;
    }
}