using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ProductType.AddProductType.Command;
using Masterov.Domain.Masterov.ProductType.GetProductTypeByName;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.AddProductType;

public class AddProductTypeUseCase(IValidator<AddProductTypeCommand> validator,
    IAddProductTypeStorage addProductTypeStorage,
    IGetProductTypeByNameStorage getProductTypeByNameStorage) : IAddProductTypeUseCase
{
    public async Task<ProductTypeDomain> Execute(AddProductTypeCommand addProductTypeCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(addProductTypeCommand, cancellationToken);

        var productType = await getProductTypeByNameStorage.GetProductTypeByName(addProductTypeCommand.Name, cancellationToken);

        if (productType is not null)
            throw new ProductTypeExistsException();
        
        
        return await addProductTypeStorage.AddProductType(addProductTypeCommand.Name, addProductTypeCommand?.Description, cancellationToken);
    }
}