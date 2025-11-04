using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models.ComponentType;
using Masterov.API.Models.Supplier;
using Masterov.API.Models.Supply;
using Masterov.API.Models.Warehouse;
using Masterov.Domain.Masterov.Supply.AddSupply;
using Masterov.Domain.Masterov.Supply.AddSupply.Command;
using Masterov.Domain.Masterov.Supply.DeleteSupply;
using Masterov.Domain.Masterov.Supply.DeleteSupply.Command;
using Masterov.Domain.Masterov.Supply.GetComponentTypeBySupplyId;
using Masterov.Domain.Masterov.Supply.GetComponentTypeBySupplyId.Query;
using Masterov.Domain.Masterov.Supply.GetSupplierBySupplyId;
using Masterov.Domain.Masterov.Supply.GetSupplierBySupplyId.Query;
using Masterov.Domain.Masterov.Supply.GetSupplies;
using Masterov.Domain.Masterov.Supply.GetSuppliesByCreatedAt;
using Masterov.Domain.Masterov.Supply.GetSuppliesByCreatedAt.Query;
using Masterov.Domain.Masterov.Supply.GetSuppliesByPrice;
using Masterov.Domain.Masterov.Supply.GetSuppliesByPrice.Query;
using Masterov.Domain.Masterov.Supply.GetSuppliesByQuantity;
using Masterov.Domain.Masterov.Supply.GetSuppliesByQuantity.Query;
using Masterov.Domain.Masterov.Supply.GetSuppliesByUpdatedAt;
using Masterov.Domain.Masterov.Supply.GetSuppliesByUpdatedAt.Query;
using Masterov.Domain.Masterov.Supply.GetSupplyById;
using Masterov.Domain.Masterov.Supply.GetSupplyById.Query;
using Masterov.Domain.Masterov.Supply.GetWarehouseBySupplyId;
using Masterov.Domain.Masterov.Supply.GetWarehouseBySupplyId.Query;
using Masterov.Domain.Masterov.Supply.UpdateSupply;
using Masterov.Domain.Masterov.Supply.UpdateSupply.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Поставка
/// </summary>
/// <param name="mapper"></param>
[ApiController]
[Route("api/supplies")]
public class SupplyController(IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить все поставки
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставках</returns>
    [HttpGet("getSupplies")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SupplyNewResponse>))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplies(
        [FromServices] IGetSuppliesUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplies = await useCase.Execute(cancellationToken);
        return Ok(supplies?.Select(mapper.Map<SupplyNewResponse>) ?? Array.Empty<SupplyNewResponse>());
    }
    
    /// <summary>
    /// Получить поставку по Id
    /// </summary>
    /// <param name="supplyId">Идентификатор поставки</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставке</returns>
    [HttpGet("getSupplyById/{supplyId:guid}")]
    [ProducesResponseType(200, Type = typeof(SupplyNewResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplyById(
        [FromRoute] Guid supplyId,
        [FromServices] IGetSupplyByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supply = await useCase.Execute(new GetSupplyByIdQuery(supplyId), cancellationToken);
        return Ok(mapper.Map<SupplyNewResponse>(supply));
    }
    
    /// <summary>
    /// Получить все поставки по количеству поставленных компонентов
    /// </summary>
    /// <param name="request">Количество поставленных компонентов</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставках</returns>
    [HttpGet("getSuppliesByQuantity")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SupplyNewResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliesByQuantity(
        [FromQuery] SuppliesByQuantityRequest request,
        [FromServices] IGetSuppliesByQuantityUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplies = await useCase.Execute(new GetSuppliesByQuantityQuery(request.Quantity), cancellationToken);
        return Ok(supplies?.Select(mapper.Map<SupplyNewResponse>) ?? Array.Empty<SupplyNewResponse>());
    }
    
    /// <summary>
    /// Получить все поставки по цене поставленных компонентов
    /// </summary>
    /// <param name="request">Цена поставки</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставках</returns>
    [HttpGet("getSuppliesByPrice")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SupplyNewResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliesByPrice(
        [FromQuery] GetSuppliesByPriceRequest request,
        [FromServices] IGetSuppliesByPriceUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplies = await useCase.Execute(new GetSuppliesByPriceQuery(request.Price), cancellationToken);
        return Ok(supplies?.Select(mapper.Map<SupplyNewResponse>) ?? Array.Empty<SupplyNewResponse>());
    }
    
    /// <summary>
    /// Получить все поставки по дате поставки
    /// </summary>
    /// <param name="request">Дата поставки</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставках</returns>
    [HttpGet("getSuppliesByCreatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SupplyNewResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliesByCreatedAt(
        [FromQuery] GetSuppliesByCreatedAtRequest request,
        [FromServices] IGetSuppliesByCreatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplies = await useCase.Execute(new GetSuppliesByCreatedAtQuery(request.CreatedAt.ToDateTime()), cancellationToken);
        return Ok(supplies?.Select(mapper.Map<SupplyNewResponse>) ?? Array.Empty<SupplyNewResponse>());
    }
    
    /// <summary>
    /// Получить все поставки по дате обновления
    /// </summary>
    /// <param name="request">Дата обновления поставки</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставках</returns>
    [HttpGet("getSuppliesByUpdatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SupplyNewResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliesByUpdatedAt(
        [FromQuery] GetSuppliesByUpdatedAtRequest request,
        [FromServices] IGetSuppliesByUpdatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplies = await useCase.Execute(new GetSuppliesByUpdatedAtQuery(request.UpdatedAt.ToDateTime()), cancellationToken);
        return Ok(supplies?.Select(mapper.Map<SupplyNewResponse>) ?? Array.Empty<SupplyNewResponse>());
    }
    
    /// <summary>
    /// Получить поставщика по идентификатору поставки
    /// </summary>
    /// <param name="request">Идентификатор поставки</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставщике</returns>
    [HttpGet("GetSupplierBySupplyId")]
    [ProducesResponseType(200, Type = typeof(SupplierNewResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplierBySupplyId(
        [FromQuery] GetSupplierBySupplyIdRequest request,
        [FromServices] IGetSupplierBySupplyIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplier = await useCase.Execute(new GetSupplierBySupplyIdQuery(request.SupplyId), cancellationToken);
        return Ok(mapper.Map<SupplierNewResponse>(supplier));
    }
    
    /// <summary>
    /// Получить поставленный тип компонента по идентификатору поставки
    /// </summary>
    /// <param name="request">Идентификатор поставки</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе компонента</returns>
    [HttpGet("GetComponentTypeBySupplyId")]
    [ProducesResponseType(200, Type = typeof(ComponentTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetComponentTypeBySupplyId(
        [FromQuery] GetComponentTypeBySupplyIdRequest request,
        [FromServices] IGetComponentTypeBySupplyIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var componentTypeDomain = await useCase.Execute(new GetComponentTypeBySupplyIdQuery(request.SupplyId), cancellationToken);
        return Ok(mapper.Map<ComponentTypeResponse>(componentTypeDomain));
    }
    
    /// <summary>
    /// Получить склад по идентификатору поставки
    /// </summary>
    /// <param name="request">Идентификатор поставки</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о складе</returns>
    [HttpGet("GetWarehouseBySupplyId")]
    [ProducesResponseType(200, Type = typeof(WarehouseNewResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetWarehouseBySupplyId(
        [FromQuery] GetWarehouseBySupplyIdRequest request,
        [FromServices] IGetWarehouseBySupplyIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var warehouseDomain = await useCase.Execute(new GetWarehouseBySupplyIdQuery(request.SupplyId), cancellationToken);
        return Ok(mapper.Map<WarehouseNewResponse>(warehouseDomain));
    }
    
    /// <summary>
    /// Добавить поставку
    /// </summary>
    /// <param name="request">Данные о поставке</param>
    /// <param name="useCase">Сценарий добавления поставки</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPost("addSupply")]
    [ProducesResponseType(201, Type = typeof(SupplyNewResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [ProducesResponseType(409, Type = typeof(ProblemDetails))]
    [ProducesResponseType(422, Type = typeof(ProblemDetails))]
    [ProducesResponseType(500, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> AddSupply(
        [FromBody] AddSupplyRequest request,
        [FromServices] IAddSupplyUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supply = await useCase.Execute(new AddSupplyCommand(request.SupplierId, request.ComponentTypeId, request.WarehouseId, request.Quantity, request.Price), cancellationToken);
    
        if (supply is null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Поставка не создана",
                Detail = "Не удалось создать поставку. Проверьте корректность введённых данных."
            });
        }

        return CreatedAtAction(nameof(GetSupplyById),
            new { supplyId = supply.SupplyId },
            mapper.Map<SupplyNewResponse>(supply));
    }
    
    /// <summary>
    /// Удаление поставки по Id
    /// </summary>
    /// <param name="supplyId">Идентификатор поставки</param>
    /// <param name="useCase">Сценарий удаления поставки</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Ответ с кодом 204, если поставка бала успешно удалена</returns>
    [HttpDelete("deleteSupply/{supplyId:guid}")]
    [ProducesResponseType(204, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [ProducesResponseType(409, Type = typeof(ProblemDetails))]
    [ProducesResponseType(422, Type = typeof(ProblemDetails))]
    [ProducesResponseType(500, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> DeleteSupply(
        [FromRoute] Guid supplyId,
        [FromServices] IDeleteSupplyUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(new DeleteSupplyCommand(supplyId), cancellationToken);
        return NoContent();
    }
    
    /// <summary>
    /// Обновить поставку по Id
    /// </summary>
    /// <param name="supplyId">Идентификатор поставки</param>
    /// <param name="request">Данные для обновления поставки</param>
    /// <param name="useCase">Сценарий обновления поставки</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обновления</returns>
    [HttpPatch("updateSupply/{supplyId:guid}")]
    [ProducesResponseType(200, Type = typeof(SupplyNewResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [ProducesResponseType(409, Type = typeof(ProblemDetails))]
    [ProducesResponseType(422, Type = typeof(ProblemDetails))]
    [ProducesResponseType(500, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdateSupply(
        [FromRoute] Guid supplyId,
        [FromBody] UpdateSupplyRequest request,
        [FromServices] IUpdateSupplyUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updateSupply = await useCase.Execute(
            new UpdateSupplyCommand(supplyId, request.SupplierId, request.ComponentTypeId, request.WarehouseId, request.Quantity, request.Price, request.CreatedAt.ToDateTime()),
            cancellationToken);
        return Ok(mapper.Map<SupplyNewResponse>(updateSupply));
    }
}