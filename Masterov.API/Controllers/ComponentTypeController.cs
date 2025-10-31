using AutoMapper;
using Masterov.API.Models.ComponentType;
using Masterov.Domain.Masterov.ComponentType.AddComponentType;
using Masterov.Domain.Masterov.ComponentType.AddComponentType.Command;
using Masterov.Domain.Masterov.ComponentType.DeleteComponentType;
using Masterov.Domain.Masterov.ComponentType.DeleteComponentType.Command;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById.Query;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeByName;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeByName.Query;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypes;
using Masterov.Domain.Masterov.ComponentType.UpdateComponentType;
using Masterov.Domain.Masterov.ComponentType.UpdateComponentType.Command;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

[ApiController]
[Route("api/ComponentTypes")]
public class ComponentTypeController(IMapper mapper): ControllerBase
{
    /// <summary>
    /// Получить все типы компонентов
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Типы компонентов</returns>
    [HttpGet("getComponentTypes")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ComponentTypeResponse>))]
    [ProducesResponseType(410)]
    public async Task<IActionResult> GetComponentTypes(
        [FromServices] IGetComponentTypesUseCase useCase,
        CancellationToken cancellationToken)
    {
        var componentTypes = await useCase.Execute(cancellationToken);
        return Ok(componentTypes.Select(mapper.Map<ComponentTypeResponse>));
    }
    
    /// <summary>
    /// Получить изделие по Id
    /// </summary>
    /// <param name="componentTypeId">Идентификатор типа изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе изделия</returns>
    [HttpGet("getComponentTypeById/{componentTypeId:guid}")]
    [ProducesResponseType(200, Type = typeof(ComponentTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetComponentTypeById(
        [FromRoute] Guid componentTypeId,
        [FromServices] IGetComponentTypeByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var componentType = await useCase.Execute(new GetComponentTypeByIdQuery(componentTypeId), cancellationToken);
        return Ok(mapper.Map<ComponentTypeResponse>(componentType));
    }
    
    /// <summary>
    /// Получить изделие по имени
    /// </summary>
    /// <param name="componentTypeName">Название типа изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе изделия</returns>
    [HttpGet("getComponentTypeByName/{componentTypeName}")]
    [ProducesResponseType(200, Type = typeof(ComponentTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetComponentTypeByName(
        [FromRoute] string componentTypeName,
        [FromServices] IGetComponentTypeByNameUseCase useCase,
        CancellationToken cancellationToken)
    {
        var componentType = await useCase.Execute(new GetComponentTypeByNameQuery(componentTypeName), cancellationToken);
        return Ok(mapper.Map<ComponentTypeResponse>(componentType));
    }
    
    /// <summary>
    /// Добавить тип изделия
    /// </summary>
    /// <param name="request">Данные о типе изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе изделия</returns>
    [HttpPost("addComponentType")]
    [ProducesResponseType(201, Type = typeof(ComponentTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(409, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> AddComponentType(
        [FromBody] AddComponentTypeRequest request,
        [FromServices] IAddComponentTypeUseCase useCase,
        CancellationToken cancellationToken)
    {
        var componentType = await useCase.Execute(new AddComponentTypeCommand(request.Name, request.Description), cancellationToken);
        return CreatedAtAction(nameof(GetComponentTypeById), new { productTypeId = componentType.ComponentTypeId }, mapper.Map<ComponentTypeResponse>(componentType));
    }
    
    /// <summary>
    /// Удалить тип изделия по Id
    /// </summary>
    /// <param name="componentTypeId">Идентификатор типа изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация выполения</returns>
    [HttpDelete("deleteComponentType/{componentTypeId:guid}/")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(410)]
    public async Task<IActionResult> DeleteComponentType(
        Guid componentTypeId,
        [FromServices] IDeleteComponentTypeUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(new DeleteComponentTypeCommand(componentTypeId), cancellationToken);
        return NoContent();
    }
    
    /// <summary>
    /// Обновить тип изделия по Id
    /// </summary>
    /// <param name="request"></param>
    /// <param name="useCase"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("updateComponentType")]
    [ProducesResponseType(200, Type = typeof(ComponentTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    public async Task<IActionResult> UpdateComponentType(
        [FromBody] UpdateComponentTypeRequest request,
        [FromServices] IUpdateProductTypeUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updatedComponentType = await useCase.Execute(new UpdateComponentTypeCommand(request.ComponentTypeId, request.Name, request.Description), cancellationToken);
        return Ok(mapper.Map<ComponentTypeResponse>(updatedComponentType));
    }
}
