using AutoMapper;
using Masterov.API.Models.ProductType;
using Masterov.Domain.Masterov.ProductType.AddProductType;
using Masterov.Domain.Masterov.ProductType.AddProductType.Command;
using Masterov.Domain.Masterov.ProductType.DeleteProductType;
using Masterov.Domain.Masterov.ProductType.DeleteProductType.Command;
using Masterov.Domain.Masterov.ProductType.GetProductsType;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById.Query;
using Masterov.Domain.Masterov.ProductType.GetProductTypeByName;
using Masterov.Domain.Masterov.ProductType.GetProductTypeByName.Query;
using Masterov.Domain.Masterov.ProductType.UpdateProductType;
using Masterov.Domain.Masterov.ProductType.UpdateProductType.Command;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

[ApiController]
[Route("api/productType")]
public class ProductTypeController(IMapper mapper): ControllerBase
{
    /// <summary>
    /// Получить все типы изделий
    /// </summary>
    /// <param name="useCase">ценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Типы изделий</returns>
    [HttpGet("getProductsType")]
    [ProducesResponseType(200, Type = typeof(ProductTypeResponse[]))]
    [ProducesResponseType(410)]
    public async Task<IActionResult> GetProductsType(
        [FromServices] IGetProductsTypeUseCase useCase,
        CancellationToken cancellationToken)
    {
        var files = await useCase.Execute(cancellationToken);
        return Ok(files.Select(mapper.Map<ProductTypeResponse>));
    }
    
    /// <summary>
    /// Получить изделие по Id
    /// </summary>
    /// <param name="productTypeId">Идентификатор типа изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе изделия</returns>
    [HttpGet("getProductTypeById/{productTypeId:guid}")]
    [ProducesResponseType(200, Type = typeof(ProductTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductTypeById(
        [FromRoute] Guid productTypeId,
        [FromServices] IGetProductTypeByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productType = await useCase.Execute(new GetProductTypeByIdQuery(productTypeId), cancellationToken);
        return Ok(mapper.Map<ProductTypeResponse>(productType));
    }
    
    /// <summary>
    /// Получить изделие по имени
    /// </summary>
    /// <param name="productTypeName">Название типа изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе изделия</returns>
    [HttpGet("getProductTypeByName/{productTypeName}")]
    [ProducesResponseType(200, Type = typeof(ProductTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductTypeByName(
        [FromRoute] string productTypeName,
        [FromServices] IGetProductTypeByNameUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productType = await useCase.Execute(new GetProductTypeByNameQuery(productTypeName), cancellationToken);
        return Ok(mapper.Map<ProductTypeResponse>(productType));
    }
    
    /// <summary>
    /// Добавить тип изделия
    /// </summary>
    /// <param name="request">Изделие</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе изделия</returns>
    [HttpPost("addProductType")]
    [ProducesResponseType(201, Type = typeof(ProductTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    public async Task<IActionResult> AddProductType(
        [FromBody] AddProductTypeRequest request,
        [FromServices] IAddProductTypeUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productType = await useCase.Execute(new AddProductTypeCommand(request.Name, request.Description), cancellationToken);
        
        return CreatedAtAction(nameof(GetProductTypeById), new { productTypeId = productType.ProductTypeId }, mapper.Map<ProductTypeResponse>(productType));
    }
    
    /// <summary>
    /// Удалить тип изделия по Id
    /// </summary>
    /// <param name="productTypeId">Идентификатор типа изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация выполения</returns>
    [HttpDelete("deleteProductType/{productTypeId:guid}/")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(410)]
    public async Task<IActionResult> DeleteProductType(
        Guid productTypeId,
        [FromServices] IDeleteProductTypeUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(new DeleteProductTypeCommand(productTypeId), cancellationToken);
        return NoContent();
    }
    
    /// <summary>
    /// Обновить тип изделия по Id
    /// </summary>
    /// <param name="request"></param>
    /// <param name="useCase"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("updateProductType")]
    [ProducesResponseType(200, Type = typeof(ProductTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    public async Task<IActionResult> UpdateProductType(
        [FromBody] UpdateProductTypeRequest request,
        [FromServices] IUpdateProductTypeUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updatedProductType = await useCase.Execute(new UpdateProductTypeCommand(request.ProductTypeId, request.Name, request.Description), cancellationToken);
        return Ok(mapper.Map<ProductTypeResponse>(updatedProductType));
    }
}
