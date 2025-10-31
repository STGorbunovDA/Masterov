using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ComponentType.AddComponentType.Command;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeByName;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.AddComponentType;

public class AddComponentTypeUseCase(IValidator<AddComponentTypeCommand> validator,
    IAddComponentTypeStorage addComponentTypeStorage,
    IGetComponentTypeByNameStorage getComponentTypeByNameStorage) : IAddComponentTypeUseCase
{
    public async Task<ComponentTypeDomain> Execute(AddComponentTypeCommand addComponentTypeCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(addComponentTypeCommand, cancellationToken);

        var componentType = await getComponentTypeByNameStorage.GetComponentTypeByName(addComponentTypeCommand.Name, cancellationToken);

        if (componentType is not null)
            throw new ProductTypeExistsException(addComponentTypeCommand.Name);
        
        return await addComponentTypeStorage.AddComponentType(addComponentTypeCommand.Name, addComponentTypeCommand?.Description, cancellationToken);
    }
}