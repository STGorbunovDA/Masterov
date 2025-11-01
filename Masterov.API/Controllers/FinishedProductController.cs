using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models.FinishedProduct;
using Masterov.API.Models.Order;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct.Command;
using Masterov.Domain.Masterov.FinishedProduct.DeleteFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.DeleteFinishedProduct.Command;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById.Query;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByIdWithoutOrders;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByIdWithoutOrders.Query;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProducts;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAt;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAt.Query;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAtWithoutOrders;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAtWithoutOrders.Query;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByName;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByName.Query;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByNameWithoutOrders;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByNameWithoutOrders.Query;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAt;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAt.Query;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAtWithoutOrders;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAtWithoutOrders.Query;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsWithoutOrders;
using Masterov.Domain.Masterov.FinishedProduct.GetOrdersByFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.GetOrdersByFinishedProduct.Query;
using Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct.Command;
using Masterov.Domain.Models;
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
    /// Получить все готовые мебельные изделия без заказов
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о всех готовых мебельных изделий без заказов</returns>
    [HttpGet("getFinishedProductsWithoutOrders")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<FinishedProductNoOrdersResponse>))]
    public async Task<IActionResult> GetFinishedProductsWithoutOrders(
        [FromServices] IGetFinishedProductsWithoutOrdersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var finishedProducts = await useCase.Execute(cancellationToken);
        return Ok(finishedProducts?.Select(mapper.Map<FinishedProductNoOrdersResponse>) ?? Array.Empty<FinishedProductNoOrdersResponse>());
    }
    
    /// <summary>
    /// Получить все готовые мебельные изделия с заказами
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о всех готовых мебельных изделий</returns>
    [HttpGet("getFinishedProducts")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<FinishedProductResponse>))]
    public async Task<IActionResult> GetFinishedProducts(
        [FromServices] IGetFinishedProductsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var finishedProducts = await useCase.Execute(cancellationToken);
        return Ok(finishedProducts?.Select(mapper.Map<FinishedProductResponse>) ?? Array.Empty<FinishedProductResponse>());
    }

    /// <summary>
    /// Получить готовое мебельное изделие по Id
    /// </summary>
    /// <param name="finishedProductId">Идентификатор изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о готовом мебельном изделии</returns>
    [HttpGet("getFinishedProductById/{finishedProductId:guid}")]
    [ProducesResponseType(200, Type = typeof(FinishedProductResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetFinishedProductById(
        [FromRoute] Guid finishedProductId,
        [FromServices] IGetFinishedProductByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var finishedProduct = await useCase.Execute(new GetFinishedProductByIdQuery(finishedProductId), cancellationToken);
        return Ok(mapper.Map<FinishedProductResponse>(finishedProduct));
    }
    
    /// <summary>
    /// Получить готовое мебельное изделие по Id без заказов
    /// </summary>
    /// <param name="finishedProductId">Идентификатор изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о готовом мебельном изделии без заказов</returns>
    [HttpGet("getFinishedProductByIdWithoutOrders/{finishedProductId:guid}")]
    [ProducesResponseType(200, Type = typeof(FinishedProductNoOrdersResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetFinishedProductByIdWithoutOrders(
        [FromRoute] Guid finishedProductId,
        [FromServices] IGetFinishedProductByIdWithoutOrdersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var finishedProduct = await useCase.Execute(new GetFinishedProductByIdWithoutOrdersQuery(finishedProductId), cancellationToken);
        return Ok(mapper.Map<FinishedProductNoOrdersResponse>(finishedProduct));
    }

    /// <summary>
    /// Получить готовое мебельное изделие по имени без заказов
    /// </summary>
    /// <param name="request">Название готового мебельного изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о готовом мебельном изделии</returns>
    [HttpGet("getFinishedProductsByNameWithoutOrders")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<FinishedProductNoOrdersResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    public async Task<IActionResult> GetFinishedProductsByNameWithoutOrders(
        [FromQuery] GetFinishedProductsByNameWithoutOrdersRequest request,
        [FromServices] IGetFinishedProductsByNameWithoutOrdersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var finishedProducts = await useCase.Execute(new GetFinishedProductsByNameWithoutOrdersQuery(request.FinishedProductName), cancellationToken);
        return Ok(finishedProducts?.Select(mapper.Map<FinishedProductNoOrdersResponse>) ?? Array.Empty<FinishedProductNoOrdersResponse>());
    }
    
    /// <summary>
    /// Получить готовое мебельное изделие по имени 
    /// </summary>
    /// <param name="request">Название готового мебельного изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о готовом мебельном изделии</returns>
    [HttpGet("getFinishedProductByName/{finishedProductName}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<FinishedProductResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    public async Task<IActionResult> GetFinishedProductsByName(
        [FromQuery] GetFinishedProductsByNameRequest request,
        [FromServices] IGetFinishedProductsByNameUseCase useCase,
        CancellationToken cancellationToken)
    {
        var finishedProducts = await useCase.Execute(new GetFinishedProductsByNameQuery(request.FinishedProductName), cancellationToken);
        return Ok(finishedProducts?.Select(mapper.Map<FinishedProductNoOrdersResponse>) ?? Array.Empty<FinishedProductNoOrdersResponse>());
    }
    
    /// <summary>
    /// Получить готовое мебельное изделие по дате создания
    /// </summary>
    /// <param name="request">Дата создания готового мебельного изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о готовых мебельных изделиях</returns>
    [HttpGet("getFinishedProductsByCreatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<FinishedProductResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    public async Task<IActionResult> GetFinishedProductsByCreatedAt(
        [FromQuery] GetFinishedProductsByCreatedAtRequest request,
        [FromServices] IGetFinishedProductsByCreatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var finishedProducts = await useCase.Execute(new GetFinishedProductsByCreatedAtQuery(request.CreatedAt.ToDateTime()), cancellationToken);
        return Ok(finishedProducts?.Select(mapper.Map<FinishedProductResponse>) ?? Array.Empty<FinishedProductResponse>());
    }
    
    /// <summary>
    /// Получить готовое мебельное изделие по дате создания без ордеров
    /// </summary>
    /// <param name="request">Дата создания готового мебельного изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о готовых мебельных изделиях</returns>
    [HttpGet("getFinishedProductsByCreatedAtWithoutOrders")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<FinishedProductNoOrdersResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    public async Task<IActionResult> GetFinishedProductsByCreatedAtWithoutOrders(
        [FromQuery] GetFinishedProductsByCreatedAtWithoutOrdersRequest request,
        [FromServices] IGetFinishedProductsByCreatedAtWithoutOrdersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var finishedProducts = await useCase.Execute(new GetFinishedProductsByCreatedAtWithoutOrdersQuery(request.CreatedAt.ToDateTime()), cancellationToken);
        return Ok(finishedProducts?.Select(mapper.Map<FinishedProductNoOrdersResponse>) ?? Array.Empty<FinishedProductNoOrdersResponse>());
    }
    
    /// <summary>
    /// Получить готовое мебельное изделие по дате обновления
    /// </summary>
    /// <param name="request">Дата обновления готового мебельного изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о готовых мебельных изделиях</returns>
    [HttpGet("getFinishedProductsByUpdatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<FinishedProductResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    public async Task<IActionResult> GetFinishedProductsByUpdatedAt(
        [FromQuery] GetFinishedProductsByUpdatedAtRequest request,
        [FromServices] IGetFinishedProductsByUpdatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var finishedProducts = await useCase.Execute(new GetFinishedProductsByUpdatedAtQuery(request.UpdatedAt.ToDateTime()), cancellationToken);
        return Ok(finishedProducts?.Select(mapper.Map<FinishedProductResponse>) ?? Array.Empty<FinishedProductResponse>());
    }
    
    /// <summary>
    /// Получить готовое мебельное изделие по дате обновления без ордеров
    /// </summary>
    /// <param name="request">Дата обновления готового мебельного изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о готовых мебельных изделиях</returns>
    [HttpGet("getFinishedProductsByUpdatedAtWithoutOrders")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<FinishedProductNoOrdersResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    public async Task<IActionResult> GetFinishedProductsByUpdatedAtWithoutOrders(
        [FromQuery] GetFinishedProductsByUpdatedAtWithoutOrdersRequest request,
        [FromServices] IGetFinishedProductsByUpdatedAtWithoutOrdersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var finishedProducts = await useCase.Execute(new GetFinishedProductsByUpdatedAtWithoutOrdersQuery(request.UpdatedAt.ToDateTime()), cancellationToken);
        return Ok(finishedProducts?.Select(mapper.Map<FinishedProductNoOrdersResponse>) ?? Array.Empty<FinishedProductNoOrdersResponse>());
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
    [ProducesResponseType(200, Type = typeof(IEnumerable<FinishedProductResponse>))]
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
                request.Status != null ? EnumTypeHelper.FromExtensionOrderStatus(request.Status) : OrderStatus.Unknown,
                request.Description
            ), 
            cancellationToken);

        return Ok(orders?.Select(mapper.Map<OrderResponse>) ?? Array.Empty<OrderResponse>());
    }

    /// <summary>
    /// Добавить готовое мебельное изделие
    /// </summary>
    /// <param name="request">Данные готового мебельного изделия</param>
    /// <param name="useCase">Сценарий добавления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPost("addFinishedProduct")]
    [ProducesResponseType(201, Type = typeof(FinishedProductResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
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
            mapper.Map<FinishedProductResponse>(finishedProduct));
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
        [FromRoute] Guid finishedProductId,
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
    [ProducesResponseType(200, Type = typeof(FinishedProductResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
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
        return Ok(mapper.Map<FinishedProductResponse>(updateFinishedProduct));
    }
}