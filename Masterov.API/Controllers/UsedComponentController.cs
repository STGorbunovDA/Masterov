using AutoMapper;
using Masterov.API.Models.UsedComponent;
using Masterov.Domain.Masterov.UsedComponent.GetComponents;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById.Query;
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
    [ProducesResponseType(200, Type = typeof(IEnumerable<UsedComponentRequest>))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetUsedComponents(
        [FromServices] IGetUsedComponentsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var usedComponents = await useCase.Execute(cancellationToken);
        return Ok(usedComponents.Select(mapper.Map<UsedComponentRequest>));
    }
    
    /// <summary>
    /// Получить используемый компонент по Id
    /// </summary>
    /// <param name="usedComponentId">Идентификатор используемого компонента</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о используемом компоненте</returns>
    [HttpGet("getUsedComponentIdById/{usedComponentId:guid}")]
    [ProducesResponseType(200, Type = typeof(UsedComponentRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetUsedComponentId(
        [FromRoute] Guid usedComponentId,
        [FromServices] IGetUsedComponentByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var customer = await useCase.Execute(new GetUsedComponentByIdQuery(usedComponentId), cancellationToken);
        return Ok(mapper.Map<UsedComponentRequest>(customer));
    }

    
}