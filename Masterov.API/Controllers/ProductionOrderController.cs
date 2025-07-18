﻿using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models.Customer;
using Masterov.API.Models.FinishedProduct;
using Masterov.API.Models.Payment;
using Masterov.API.Models.ProductComponent;
using Masterov.API.Models.ProductionOrder;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId.Query;
using Masterov.Domain.Masterov.ProductionOrder.AddProductionOrder;
using Masterov.Domain.Masterov.ProductionOrder.AddProductionOrder.Command;
using Masterov.Domain.Masterov.ProductionOrder.DeleteProductionOrder;
using Masterov.Domain.Masterov.ProductionOrder.DeleteProductionOrder.Command;
using Masterov.Domain.Masterov.ProductionOrder.GetCustomerByOrderId;
using Masterov.Domain.Masterov.ProductionOrder.GetCustomerByOrderId.Query;
using Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductAtOrder;
using Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductAtOrder.Query;
using Masterov.Domain.Masterov.ProductionOrder.GetProductComponentAtOrder;
using Masterov.Domain.Masterov.ProductionOrder.GetProductComponentAtOrder.Query;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById.Query;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrders;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCompletedAt;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCompletedAt.Query;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCreatedAt;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCreatedAt.Query;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByDescription;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByDescription.Query;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByStatus;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByStatus.Query;
using Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrder;
using Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrder.Command;
using Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrderStatus;
using Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrderStatus.Command;
using Masterov.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Ордера (заказы)
/// </summary>
/// <param name="mapper"></param>
[ApiController]
[Route("api/productionOrder")]
public class ProductionOrderController(IMapper mapper) : ControllerBase
{
    // TODO если статус Canceled тогда все компоненты должны вернуться на склад с которого взяли и соответсвенно?

    /// <summary>
    /// Получить все заказы
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о всех заказах (ордерах)</returns>
    [HttpGet("getProductionOrders")]
    [ProducesResponseType(200, Type = typeof(ProductionOrderRequest[]))]
    [ProducesResponseType(410)]
    public async Task<IActionResult> GetProductionOrders(
        [FromServices] IGetProductionOrdersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var files = await useCase.Execute(cancellationToken);
        return Ok(files.Select(mapper.Map<ProductionOrderRequest>));
    }

    /// <summary>
    /// Получить ордер (заказ) по Id
    /// </summary>
    /// <param name="orderId">Идентификатор ордера (заказа)</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказе (ордере)</returns>
    [HttpGet("getProductionOrderByOrderId/{orderId:guid}")]
    [ProducesResponseType(200, Type = typeof(ProductionOrderRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductionOrderByOrderId(
        [FromRoute] Guid orderId,
        [FromServices] IGetProductionOrderByOrdeIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var order = await useCase.Execute(new GetProductionOrderByOrderIdQuery(orderId), cancellationToken);
        return Ok(mapper.Map<ProductionOrderRequest>(order));
    }

    /// <summary>
    /// Получить список ордеров (заказов) по дате создания
    /// </summary>
    /// <param name="request">Дата создания ордера (заказа)</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказах (ордерах)</returns>
    [HttpGet("getProductionOrdersByCreatedAt")]
    [ProducesResponseType(200, Type = typeof(ProductionOrderRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductionOrdersByCreatedAt(
        [FromQuery] GetProductionOrderByCreatedAtRequest request,
        [FromServices] IGetProductionOrdersByCreatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders =
            await useCase.Execute(new GetProductionOrdersByCreatedAtQuery(request.CreatedAt), cancellationToken);
        return Ok(orders?.Select(mapper.Map<ProductionOrderRequest>) ?? Array.Empty<ProductionOrderRequest>());
    }

    /// <summary>
    /// Получить список ордеров (заказов) по дате закрытия
    /// </summary>
    /// <param name="request">Дата закрытия ордера (заказа)</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказах (ордерах)</returns>
    [HttpGet("getProductionOrdersByCompletedAt")]
    [ProducesResponseType(200, Type = typeof(ProductionOrderRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductionOrdersByCompletedAt(
        [FromQuery] GetProductionOrderByCompletedAtRequest request,
        [FromServices] IGetProductionOrdersByCompletedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders = await useCase.Execute(new GetProductionOrdersByCompletedAtQuery(request.CompletedAt),
            cancellationToken);
        return Ok(orders?.Select(mapper.Map<ProductionOrderRequest>) ?? Array.Empty<ProductionOrderRequest>());
    }

    /// <summary>
    /// Получить список ордеров (заказов) по описанию
    /// </summary>
    /// <param name="request">Описание ордера (заказа)</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказах (ордерах)</returns>
    [HttpGet("getProductionOrdersByDescription")]
    [ProducesResponseType(200, Type = typeof(ProductionOrderRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductionOrdersByDescription(
        [FromQuery] GetProductionOrderByDescriptionRequest request,
        [FromServices] IGetProductionOrdersByDescriptionUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders = await useCase.Execute(new GetProductionOrdersByDescriptionQuery(request.Description),
            cancellationToken);
        return Ok(orders?.Select(mapper.Map<ProductionOrderRequest>) ?? Array.Empty<ProductionOrderRequest>());
    }

    /// <summary>
    /// Получить список ордеров (заказов) по статусу
    /// </summary>
    /// <param name="request">Статус ордера (заказа)</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказах (ордерах)</returns>
    [HttpGet("getProductionOrdersByStatus")]
    [ProducesResponseType(200, Type = typeof(ProductionOrderRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductionOrdersByStatus(
        [FromQuery] GetProductionOrderByStatusRequest request,
        [FromServices] IGetProductionOrdersByStatusUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders =
            await useCase.Execute(
                new GetProductionOrdersByStatusQuery(EnumTypeHelper.FromExtensionProductionOrderStatus(request.Status)),
                cancellationToken);
        return Ok(orders?.Select(mapper.Map<ProductionOrderRequest>) ?? Array.Empty<ProductionOrderRequest>());
    }

    /// <summary>
    /// Получить готовое мебельное изделие у ордера
    /// </summary>
    /// <param name="request">Id Ордера (заказа)</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Готовое мебельное изделие</returns>
    [HttpGet("getFinishedProductAtOrder")]
    [ProducesResponseType(200, Type = typeof(FinishedProductRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetFinishedProductAtOrder(
        [FromQuery] GetFinishedProductAtOrderRequest request,
        [FromServices] IGetFinishedProductAtOrderUseCase useCase,
        CancellationToken cancellationToken)
    {
        var product = await useCase.Execute(new GetFinishedProductAtOrderQuery(request.OrderId), cancellationToken);
        return Ok(mapper.Map<FinishedProductRequest>(product));
    }

    /// <summary>
    /// Получить используемые компоненты заказа по OrderId
    /// </summary>
    /// <param name="idRequest">Id Ордера (заказа)</param>
    /// <param name="idUseCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Используемые компоненты</returns>
    [HttpGet("getProductComponentByOrderId")]
    [ProducesResponseType(200, Type = typeof(ProductComponentDomain[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductComponentByOrderId(
        [FromQuery] GetProductComponentByOrderIdRequest idRequest,
        [FromServices] IGetProductComponentByOrderIdUseCase idUseCase,
        CancellationToken cancellationToken)
    {
        var productionComponents =
            await idUseCase.Execute(new GetProductComponentByOrderIdQuery(idRequest.OrderId), cancellationToken);
        return Ok(mapper.Map<IEnumerable<ProductComponentRequest>>(productionComponents));
    }

    /// <summary>
    /// Получить заказчика по идентификатору заказа (ордера)
    /// </summary>
    /// <param name="orderId">Идентификатор ордера (заказа)</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказчике</returns>
    [HttpGet("getCustomerByOrderId/{orderId:guid}")]
    [ProducesResponseType(200, Type = typeof(CustomerNoOrdersRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCustomerByOrderId(
        [FromRoute] Guid orderId,
        [FromServices] IGetCustomerByOrderIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var customer = await useCase.Execute(new GetCustomerByOrderIdQuery(orderId), cancellationToken);
        return Ok(mapper.Map<CustomerNoOrdersRequest>(customer));
    }

    /// <summary>
    /// Получить платежи по Идентификатору заказа
    /// </summary>
    /// <param name="request">Идентификатор заказа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о ордере</returns>
    [HttpGet("getPaymentsByOrderId")]
    [ProducesResponseType(200, Type = typeof(PaymentRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    //[Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetPaymentsByOrderId(
        [FromQuery] GetPaymentsByOrderIdRequest request,
        [FromServices] IGetPaymentsByOrderIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var payments =
            await useCase.Execute(new GetPaymentsByOrderIdQuery(request.OrderId), cancellationToken);
        return Ok(payments?.Select(mapper.Map<PaymentRequest>) ?? Array.Empty<PaymentRequest>());
    }

    /// <summary>
    /// Добавить ордер
    /// </summary>
    /// <param name="request">Данные ордера</param>
    /// <param name="useCase">Сценарий добавления ордера</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPost("addProductionOrder")]
    [ProducesResponseType(201, Type = typeof(ProductionOrderRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> AddProductionOrder(
        [FromForm] AddProductionOrderRequest request,
        [FromServices] IAddProductionOrderUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productionOrder = await useCase.Execute(
            new AddProductionOrderCommand(request.FinishedProductId, request.Description, request.CustomerId,
                request.CustomerName, request.CustomerPhone, request.CustomerEmail), cancellationToken);

        return CreatedAtAction(nameof(GetProductionOrderByOrderId),
            new { orderId = productionOrder.OrderId },
            mapper.Map<ProductionOrderRequest>(productionOrder));
    }

    /// <summary>
    /// Обновить статус у ордера (заказа)
    /// </summary>
    /// <param name="request">Данные для обновления статуса заказа</param>
    /// <param name="useCase">Сценарий обновления статуса заказа</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPatch("updateProductionOrderStatus")]
    [ProducesResponseType(201, Type = typeof(ProductionOrderRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    //[Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdateProductionOrderStatus(
        [FromForm] UpdateProductionOrderStatusRequest request,
        [FromServices] IUpdateProductionOrderStatusUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productionOrder = await useCase.Execute(
            new UpdateProductionOrderStatusCommand(request.OrderId,
                EnumTypeHelper.FromExtensionProductionOrderStatus(request.Status)), cancellationToken);

        return CreatedAtAction(nameof(GetProductionOrderByOrderId),
            new { orderId = productionOrder.OrderId },
            mapper.Map<ProductionOrderRequest>(productionOrder));
    }

    /// <summary>
    /// Обновить ордер (заказ)
    /// </summary>
    /// <param name="request">Данные для обновления заказа (ордера)</param>
    /// <param name="useCase">Сценарий обновления заказа (ордера)</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPatch("updateProductionOrder")]
    public async Task<IActionResult> UpdateProductionOrder(
        [FromForm] UpdateProductionOrderRequest request,
        [FromServices] IUpdateProductionOrderUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productionOrder = await useCase.Execute(
            new UpdateProductionOrderCommand(
                request.OrderId, 
                request.CreatedAt,
                EnumTypeHelper.FromExtensionProductionOrderStatus(request.Status), 
                request.Description,
                request.CustomerId),
            cancellationToken);

        return CreatedAtAction(nameof(GetProductionOrderByOrderId),
            new { orderId = productionOrder.OrderId },
            mapper.Map<ProductionOrderRequest>(productionOrder));
    }

    /// <summary>
    /// Удаление заказа по указанному Id.
    /// </summary>
    /// <param name="productionOrderId">Идентификатор заказа по Id.</param>
    /// <param name="useCase">Сценарий удаления заказа.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Ответ с кодом 204, если заказ был успешно удален.</returns>
    [HttpDelete("DeleteProductionOrder")]
    //[Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> DeleteProductionOrder(
        Guid productionOrderId,
        [FromServices] IDeleteProductionOrderUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(new DeleteProductionOrderCommand(productionOrderId), cancellationToken);
        return NoContent();
    }
}