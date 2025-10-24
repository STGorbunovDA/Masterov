using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;
using Masterov.Domain.Masterov.ComponentType.UpdateComponentType.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.UpdateComponentType;

public class UpdateProductTypeUseCase(IValidator<UpdateComponentTypeCommand> validator,
    IUpdateProductTypeStorage updateProductTypeStorage, IGetComponentTypeByIdStorage getComponentTypeByIdStorage) : IUpdateProductTypeUseCase
{
    public async Task<ComponentTypeDomain> Execute(UpdateComponentTypeCommand updateComponentTypeCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updateComponentTypeCommand, cancellationToken);
        
        var componentTypeExists = await getComponentTypeByIdStorage.GetComponentTypeById(updateComponentTypeCommand.ComponentTypeId, cancellationToken);

        if (componentTypeExists is null)
            throw new NotFoundByIdException(updateComponentTypeCommand.ComponentTypeId, "Тип изделия");

        return await updateProductTypeStorage.UpdateComponentType(updateComponentTypeCommand.ComponentTypeId, updateComponentTypeCommand.Name, updateComponentTypeCommand.Description, cancellationToken);
    }
}