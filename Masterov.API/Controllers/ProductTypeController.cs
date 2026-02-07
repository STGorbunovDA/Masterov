using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models.ComponentType;
using Masterov.API.Models.ProductType;
using Masterov.Domain.Masterov.ComponentType.AddComponentType;
using Masterov.Domain.Masterov.ComponentType.AddComponentType.Command;
using Masterov.Domain.Masterov.ComponentType.DeleteComponentType;
using Masterov.Domain.Masterov.ComponentType.DeleteComponentType.Command;
using Masterov.Domain.Masterov.ComponentType.UpdateComponentType;
using Masterov.Domain.Masterov.ComponentType.UpdateComponentType.Command;
using Masterov.Domain.Masterov.ProductType.AddProductType;
using Masterov.Domain.Masterov.ProductType.AddProductType.Command;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById.Query;
using Masterov.Domain.Masterov.ProductType.GetProductTypes;
using Masterov.Domain.Masterov.ProductType.GetProductTypesByCreatedAt;
using Masterov.Domain.Masterov.ProductType.GetProductTypesByCreatedAt.Query;
using Masterov.Domain.Masterov.ProductType.GetProductTypesByName;
using Masterov.Domain.Masterov.ProductType.GetProductTypesByName.Query;
using Masterov.Domain.Masterov.ProductType.GetProductTypesByUpdatedAt;
using Masterov.Domain.Masterov.ProductType.GetProductTypesByUpdatedAt.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

[ApiController]
[Route("api/ProductTypes")]
public class ProductTypesController(IMapper mapper): ControllerBase
{
    /// <summary>
    /// Получить все типы готовых мебельных изделий
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Типы готовых мебельных изделий</returns>
    [HttpGet("getProductTypes")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ProductTypeResponse>))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetProductTypes(
        [FromServices] IGetProductTypesUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productTypes = await useCase.Execute(cancellationToken);
        return Ok(productTypes?.Select(mapper.Map<ProductTypeResponse>) ?? Array.Empty<ProductTypeResponse>());
    }
    
    /// <summary>
    /// Получить тип готового мебельного изделия по Id
    /// </summary>
    /// <param name="productTypeId">Идентификатор типа мебельного изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе готового мебельного изделия</returns>
    [HttpGet("getProductTypeById/{productTypeId:guid}")]
    [ProducesResponseType(200, Type = typeof(ProductTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetProductTypeById(
        [FromRoute] Guid productTypeId,
        [FromServices] IGetProductTypeByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productType = await useCase.Execute(new GetProductTypeByIdQuery(productTypeId), cancellationToken);
        return Ok(mapper.Map<ProductTypeResponse>(productType));
    }
    
    /// <summary>
    /// Получить типы готового мебельного изделия по имени
    /// </summary>
    /// <param name="request">Название типа готового мебельного изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе готового мебельного изделия</returns>
    [HttpGet("getProductTypesByName")]
    [ProducesResponseType(200, Type = typeof(ProductTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetProductTypesByName(
        [FromQuery] GetProductTypesByNameRequest request,
        [FromServices] IGetProductTypesByNameUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productType = await useCase.Execute(new GetProductTypesByNameQuery(request.ProductTypeName), cancellationToken);
        return Ok(productType?.Select(mapper.Map<ProductTypeResponse>) ?? Array.Empty<ProductTypeResponse>());
    }
    
    /// <summary>
    /// Получить типы готового мебельного изделия по дате создания
    /// </summary>
    /// <param name="request">Дата создания типа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе готового мебельного изделия</returns>
    [HttpGet("getProductTypesByCreatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ProductTypeResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    public async Task<IActionResult> GetProductTypesByCreatedAt(
        [FromQuery] GetProductTypesByCreatedAtRequest request,
        [FromServices] IGetProductTypesByCreatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productTypes = await useCase.Execute(new GetProductTypesByCreatedAtQuery(request.CreatedAt.ToDateTime()), cancellationToken);
        return Ok(productTypes?.Select(mapper.Map<ProductTypeResponse>) ?? Array.Empty<ProductTypeResponse>());
    }
    
    /// <summary>
    /// Получить типы готового мебельного изделия по дате обновления
    /// </summary>
    /// <param name="request">Дата обновления типа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе готового мебельного изделия</returns>
    [HttpGet("getProductTypesByUpdatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ProductTypeResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    public async Task<IActionResult> GetProductTypesByUpdatedAt(
        [FromQuery] GetProductTypesByUpdatedAtRequest request,
        [FromServices] IGetProductTypesByUpdatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productTypes = await useCase.Execute(new GetProductTypesByUpdatedAtQuery(request.UpdatedAt.ToDateTime()), cancellationToken);
        return Ok(productTypes?.Select(mapper.Map<ProductTypeResponse>) ?? Array.Empty<ProductTypeResponse>());
    }
    
    /// <summary>
    /// Добавить тип готового мебельного изделия
    /// </summary>
    /// <param name="request">Данные о типе готового мебельного изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе изделия</returns>
    [HttpPost("addProductType")]
    [ProducesResponseType(201, Type = typeof(ProductTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(409, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> AddProductType(
        [FromBody] AddProductTypeRequest request,
        [FromServices] IAddProductTypeUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productType = await useCase.Execute(new AddProductTypeCommand(request.Name), cancellationToken);
        return CreatedAtAction(nameof(GetProductTypeById), new { productTypeId = productType.ProductTypeId }, 
            mapper.Map<ProductTypeResponse>(productType));
    }
    
    /// <summary>
    /// Удалить тип изделия по Id
    /// </summary>
    /// <param name="componentTypeId">Идентификатор типа изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация выполения</returns>
    [HttpDelete("deleteComponentType/{componentTypeId:guid}")]
    [ProducesResponseType(204, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> DeleteComponentType(
        [FromRoute] Guid componentTypeId,
        [FromServices] IDeleteComponentTypeUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(new DeleteComponentTypeCommand(componentTypeId), cancellationToken);
        return NoContent();
    }
    
    /// <summary>
    /// Обновить тип изделия по Id
    /// </summary>
    /// <param name="componentTypeId">Идентификатор типа изделия</param>
    /// <param name="request">Данные для обновления типа изделия</param>
    /// <param name="useCase">Сценарий обновления типа изделия</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обновления типа изделия</returns>
    [HttpPatch("updateComponentType/{componentTypeId:guid}")]
    [ProducesResponseType(200, Type = typeof(ComponentTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdateComponentType(
        [FromRoute] Guid componentTypeId,
        [FromBody] UpdateComponentTypeRequest request,
        [FromServices] IUpdateComponentTypeUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updatedComponentType = await useCase.Execute(new UpdateComponentTypeCommand(componentTypeId, request.Name, request.CreatedAt.ToDateTime(), request.Description), cancellationToken);
        return Ok(mapper.Map<ComponentTypeResponse>(updatedComponentType));
    }
}
