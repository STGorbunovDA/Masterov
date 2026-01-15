using FluentValidation;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByDescription.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByDescription;

public class GetFinishedProductsByDescriptionUseCase(IValidator<GetFinishedProductsByDescriptionQuery> validator,
    IGetFinishedProductsByDescriptionStorage storage) : IGetFinishedProductsByDescriptionUseCase
{
    public async Task<IEnumerable<FinishedProductDomain>?> Execute(GetFinishedProductsByDescriptionQuery getFinishedProductsByDescriptionQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getFinishedProductsByDescriptionQuery, cancellationToken);
        return await storage.GetFinishedProductsByDescription(getFinishedProductsByDescriptionQuery.Description, cancellationToken);
    }
}