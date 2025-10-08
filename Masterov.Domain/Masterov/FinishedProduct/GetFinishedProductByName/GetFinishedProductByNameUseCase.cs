using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByName;

public class GetFinishedProductByNameUseCase(IValidator<GetFinishedProductByNameQuery> validator, IGetFinishedProductByNameStorage storage) : IGetFinishedProductByNameUseCase
{
    public async Task<IEnumerable<FinishedProductDomain?>> Execute(GetFinishedProductByNameQuery getFinishedProductByNameQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getFinishedProductByNameQuery, cancellationToken);
        var finishedProductExists = await storage.GetFinishedProductByName(getFinishedProductByNameQuery.FinishedProductName, cancellationToken);
        
        if (finishedProductExists is null)
            throw new NotFoundByNameException(getFinishedProductByNameQuery.FinishedProductName, "Готовое мебельное изделие");
        
        return finishedProductExists;
    }
}