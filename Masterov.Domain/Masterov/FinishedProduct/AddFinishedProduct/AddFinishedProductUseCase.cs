using FluentValidation;
using Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct;

public class AddFinishedProductUseCase(
    IValidator<AddFinishedProductCommand> validator,
    IAddFinishedProductStorage addFinishedProductStorage) : IAddFinishedProductUseCase
{
    public async Task<FinishedProductDomain> Execute(AddFinishedProductCommand addFinishedProductCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(addFinishedProductCommand, cancellationToken);

        return await addFinishedProductStorage.AddFinishedProduct(addFinishedProductCommand.Name,
            addFinishedProductCommand.Type,
            addFinishedProductCommand?.Price, addFinishedProductCommand?.Width, addFinishedProductCommand?.Height,
            addFinishedProductCommand?.Depth, addFinishedProductCommand?.Image, addFinishedProductCommand!.Elite,
            addFinishedProductCommand.Description, cancellationToken);
    }
}