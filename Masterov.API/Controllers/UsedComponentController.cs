using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models.UsedComponent;
using Masterov.Domain.Masterov.UsedComponent.GetComponents;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById.Query;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByCreatedAt;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByCreatedAt.Query;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByQuantity;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByQuantity.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Используемые компоненты
/// </summary>
/// <param name="mapper"></param>
[ApiController]
[Route("api/usedComponents")]
public class UsedComponentController(IMapper mapper) : ControllerBase
{
    // TODO если статус Canceled тогда все компоненты должны вернуться на склад с которого взяли и соответсвенно?
    // TODO добавить контроллер используемых компонентов и соответственно когда компоненты используют вычитать со склада то кол-во которые используются
    /// <summary>
    /// Получить все компоненты
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о компонентах</returns>
    [HttpGet("getUsedComponents")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UsedComponentResponse>))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetUsedComponents(
        [FromServices] IGetUsedComponentsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var usedComponents = await useCase.Execute(cancellationToken);
        return Ok(usedComponents.Select(mapper.Map<UsedComponentResponse>));
    }
    
    /// <summary>
    /// Получить используемый компонент по Id
    /// </summary>
    /// <param name="usedComponentId">Идентификатор используемого компонента</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о используемом компоненте</returns>
    [HttpGet("getUsedComponentIdById/{usedComponentId:guid}")]
    [ProducesResponseType(200, Type = typeof(UsedComponentResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetUsedComponentId(
        [FromRoute] Guid usedComponentId,
        [FromServices] IGetUsedComponentByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var customer = await useCase.Execute(new GetUsedComponentByIdQuery(usedComponentId), cancellationToken);
        return Ok(mapper.Map<UsedComponentResponse>(customer));
    }

    /// <summary>
    /// Получить используемые компоненты по количеству
    /// </summary>
    /// <param name="request">Количество используемых компонентов</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о используемых компонентах</returns>
    [HttpGet("getUsedComponentsByQuantity")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UsedComponentResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetUsedComponentsByQuantity(
        [FromQuery] UsedComponentsByQuantityRequest request,
        [FromServices] IGetUsedComponentsByQuantityUseCase useCase,
        CancellationToken cancellationToken)
    {
        var usedComponents = await useCase.Execute(new GetUsedComponentsByQuantityQuery(request.Quantity), cancellationToken);
        return Ok(usedComponents.Select(mapper.Map<UsedComponentResponse>));
    }
    
    /// <summary>
    /// Получить используемые компоненты по дате создания
    /// </summary>
    /// <param name="request">Дата создания используемых компонентов</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о используемых компонентах</returns>
    [HttpGet("getUsedComponentsByCreatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UsedComponentResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetUsedComponentsByCreatedAt(
        [FromQuery] GetUsedComponentsByCreatedAtRequest request,
        [FromServices] IGetUsedComponentsByCreatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var usedComponents = await useCase.Execute(new GetUsedComponentsByCreatedAtQuery(request.CreatedAt.ToDateTime()), cancellationToken);
        return Ok(usedComponents?.Select(mapper.Map<UsedComponentResponse>) ?? Array.Empty<UsedComponentResponse>());
    }

}