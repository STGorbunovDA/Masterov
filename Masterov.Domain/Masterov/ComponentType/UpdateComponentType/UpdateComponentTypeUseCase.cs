using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;
using Masterov.Domain.Masterov.ComponentType.UpdateComponentType.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.UpdateComponentType;

public class UpdateComponentTypeUseCase(
    IValidator<UpdateComponentTypeCommand> validator,
    IUpdateComponentTypeStorage updateComponentTypeStorage,
    IGetComponentTypeByIdStorage getComponentTypeByIdStorage) : IUpdateComponentTypeUseCase
{
    public async Task<ComponentTypeDomain> Execute(UpdateComponentTypeCommand updateComponentTypeCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updateComponentTypeCommand, cancellationToken);

        var componentTypeExists =
            await getComponentTypeByIdStorage.GetComponentTypeById(updateComponentTypeCommand.ComponentTypeId,
                cancellationToken);

        if (componentTypeExists is null)
            throw new NotFoundByIdException(updateComponentTypeCommand.ComponentTypeId, "Тип изделия");

        return await updateComponentTypeStorage.UpdateComponentType(updateComponentTypeCommand.ComponentTypeId,
            updateComponentTypeCommand.Name, updateComponentTypeCommand.CreatedAt, updateComponentTypeCommand.Description, cancellationToken);
    }
}