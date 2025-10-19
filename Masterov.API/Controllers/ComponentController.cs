using AutoMapper;
using Masterov.API.Models.Component;
using Masterov.Domain.Masterov.Component.GetComponents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Компоненты
/// </summary>
/// <param name="mapper"></param>
[ApiController]
[Route("api/components")]
public class ComponentController(IMapper mapper) : ControllerBase
{
    //TODO UsedComponents
    // TODO если статус Canceled тогда все компоненты должны вернуться на склад с которого взяли и соответсвенно?
    // TODO добавить контроллер используемых компонентов и соответственно когда компоненты используют вычитать со склада то кол-во которые используются
    /// <summary>
    /// Получить все компоненты
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о компонентах</returns>
    [HttpGet("getComponents")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ComponentRequest>))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetComponents(
        [FromServices] IGetComponentsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var components = await useCase.Execute(cancellationToken);
        return Ok(components.Select(mapper.Map<ComponentRequest>));
    }
    
    
}