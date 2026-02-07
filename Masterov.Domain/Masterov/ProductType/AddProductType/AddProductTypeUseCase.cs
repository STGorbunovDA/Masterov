using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ProductType.AddProductType.Command;
using Masterov.Domain.Masterov.ProductType.GetProductTypesByName;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.AddProductType;

public class AddProductTypeUseCase(IValidator<AddProductTypeCommand> validator,
    IAddProductTypeStorage addProductTypeStorage,
    IGetProductTypesByNameStorage getProductTypesByNameStorage) : IAddProductTypeUseCase
{
    public async Task<ProductTypeDomain> Execute(AddProductTypeCommand addProductTypeCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(addProductTypeCommand, cancellationToken);

        var existingProductType = (await getProductTypesByNameStorage
                .GetProductTypesByName(addProductTypeCommand.Name, cancellationToken))
            .FirstOrDefault();

        if (existingProductType is not null)
            throw new ProductTypeExistsException(existingProductType.Name);
        
        return await addProductTypeStorage.AddProductType(addProductTypeCommand.Name, cancellationToken);
    }
}