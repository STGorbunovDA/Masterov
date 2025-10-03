using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models.FinishedProduct;
using Masterov.API.Models.ProductionOrder;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct.Command;
using Masterov.Domain.Masterov.FinishedProduct.DeleteFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.DeleteFinishedProduct.Command;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById.Query;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByName;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByName.Query;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProducts;
using Masterov.Domain.Masterov.FinishedProduct.GetOrdersByFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.GetOrdersByFinishedProduct.Query;
using Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Готовое мебельное изделие
/// </summary>
/// <param name="mapper"></param>
[ApiController]
[Route("api/finishedProducts")]
public class FinishedProductController(IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить все готовые мебельные изделия
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о всех готовых мебельных изделий</returns>
    [HttpGet("getFinishedProducts")]
    [ProducesResponseType(200, Type = typeof(FinishedProductRequest[]))]
    [ProducesResponseType(410, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetFinishedProducts(
        [FromServices] IGetFinishedProductsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var finishedProducts = await useCase.Execute(cancellationToken);
        return Ok(finishedProducts.Select(mapper.Map<FinishedProductRequest>));
    }

    /// <summary>
    /// Получить готовое мебельное изделие по Id
    /// </summary>
    /// <param name="finishedProductId">Идентификатор изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о готовом мебельном изделии</returns>
    [HttpGet("getFinishedProductById/{finishedProductId:guid}")]
    [ProducesResponseType(200, Type = typeof(FinishedProductRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetFinishedProductById(
        [FromRoute] Guid finishedProductId,
        [FromServices] IGetFinishedProductByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var product = await useCase.Execute(new GetFinishedProductByIdQuery(finishedProductId), cancellationToken);
        return Ok(mapper.Map<FinishedProductRequest>(product));
    }

    /// <summary>
    /// Получить готовое мебельное изделие по имени
    /// </summary>
    /// <param name="finishedProductName">Название готового мебельного изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о готовом мебельном изделии</returns>
    [HttpGet("getFinishedProductByName/{finishedProductName}")]
    [ProducesResponseType(200, Type = typeof(FinishedProductRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetFinishedProductByName(
        [FromRoute] string finishedProductName,
        [FromServices] IGetFinishedProductByNameUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productType = await useCase.Execute(new GetFinishedProductByNameQuery(finishedProductName), cancellationToken);
        return Ok(mapper.Map<FinishedProductRequest>(productType));
    }
    
    /// <summary>
    /// Получить список ордеров готового изделия с возможностью фильтрации по Id || даты создания || даты выполнения || Статуса || Описания
    /// </summary>
    /// <param name="finishedProductId">Идентификатор готового мебельного изделия</param>
    /// <param name="request">Данные для получения ордеров готового мебельного изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат получения списка ордеров готового мебельного изделия</returns>
    [HttpGet("getOrdersByFinishedProduct/{finishedProductId:guid}")]
    [ProducesResponseType(200, Type = typeof(ProductionOrderRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetOrdersByFinishedProduct(
        [FromRoute] Guid finishedProductId,
        [FromQuery] GetOrdersByFinishedProductRequest request,
        [FromServices] IGetOrdersByFinishedProductUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders = await useCase.Execute(
            new GetOrdersByFinishedProductQuery(
                finishedProductId, 
                request.CreatedAt, 
                request.CompletedAt,
                request.Status != null ? EnumTypeHelper.FromExtensionProductionOrderStatus(request.Status) : ProductionOrderStatus.Unknown,
                request.Description
            ), 
            cancellationToken);

        return Ok(orders?.Select(mapper.Map<ProductionOrderRequest>) ?? Array.Empty<ProductionOrderRequest>());
    }

    /// <summary>
    /// Добавить готовое мебельное изделие
    /// </summary>
    /// <param name="request">Данные готового мебельного изделия</param>
    /// <param name="useCase">Сценарий добавления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPost("addFinishedProduct")]
    [ProducesResponseType(201, Type = typeof(FinishedProductRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> AddFinishedProduct(
        [FromForm] AddFinishedProductRequest request,
        [FromServices] IAddFinishedProductUseCase useCase,
        CancellationToken cancellationToken)
    {
        if (request.Image is { Length: 0 })
            return BadRequest("Изображение изделия не загружено или пустое изображение");

        if (request.Image is { Length: > 100 * 1024 * 1024 })
            return BadRequest("Изображение должно быть не более 100 мб.");

        var finishedProduct = await useCase.Execute(
            new AddFinishedProductCommand(
                request.Name,
                request.Price,
                request.Width,
                request.Height,
                request.Depth,
                request.Image == null ? null : await request.Image.ToByteArrayAsync()),
            cancellationToken);

        return CreatedAtAction(nameof(GetFinishedProductById),
            new { finishedProductId = finishedProduct.FinishedProductId },
            mapper.Map<FinishedProductRequest>(finishedProduct));
    }

    /// <summary>
    /// Удаление готового мебельного изделия по указанному Id
    /// </summary>
    /// <param name="finishedProductId">Идентификатор готового мебельного изделия</param>
    /// <param name="useCase">Сценарий удаления</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Ответ с кодом 204, если готовое мебельное изделие было успешно удалено</returns>
    [HttpDelete("deleteFinishedProduct/{finishedProductId:guid}")]
    [ProducesResponseType(204, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> DeleteFinishedProduct(
        Guid finishedProductId,
        [FromServices] IDeleteFinishedProductUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(new DeleteFinishedProductCommand(finishedProductId), cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Обновить готовое мебельное изделие по Id
    /// </summary>
    /// <param name="finishedProductId">Идентификатор готового мебельного изделия</param>
    /// <param name="request">Данные для обновления готового мебельного изделия</param>
    /// <param name="useCase">Сценарий обновления готового мебельного изделия</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обновления</returns>
    [HttpPatch("updateFinishedProduct/{finishedProductId:guid}")]
    [ProducesResponseType(200, Type = typeof(FinishedProductRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdateFinishedProduct(
        [FromRoute] Guid finishedProductId,
        [FromForm] UpdateFinishedProductRequest request,
        [FromServices] IUpdateFinishedProductUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updateFinishedProduct = await useCase.Execute(
            new UpdateFinishedProductCommand(finishedProductId, request.Name, request.Price, request.Width,
                request.Height, request.Depth, request.Image == null ? null : await request.Image.ToByteArrayAsync(), 
                request.CreatedAt?.ToDateTime()),
            cancellationToken);
        return Ok(mapper.Map<FinishedProductRequest>(updateFinishedProduct));
    }
}