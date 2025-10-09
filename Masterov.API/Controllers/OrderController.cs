using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models.Customer;
using Masterov.API.Models.FinishedProduct;
using Masterov.API.Models.Order;
using Masterov.API.Models.Payment;
using Masterov.API.Models.ProductComponent;
using Masterov.Domain.Masterov.Order.AddOrder;
using Masterov.Domain.Masterov.Order.AddOrder.Command;
using Masterov.Domain.Masterov.Order.DeleteOrder;
using Masterov.Domain.Masterov.Order.DeleteOrder.Command;
using Masterov.Domain.Masterov.Order.GetCustomerByOrderId;
using Masterov.Domain.Masterov.Order.GetCustomerByOrderId.Query;
using Masterov.Domain.Masterov.Order.GetFinishedProductByOrderId;
using Masterov.Domain.Masterov.Order.GetFinishedProductByOrderId.Query;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Masterov.Order.GetOrderById.Query;
using Masterov.Domain.Masterov.Order.GetOrders;
using Masterov.Domain.Masterov.Order.GetOrdersByCompletedAt;
using Masterov.Domain.Masterov.Order.GetOrdersByCompletedAt.Query;
using Masterov.Domain.Masterov.Order.GetOrdersByCreatedAt;
using Masterov.Domain.Masterov.Order.GetOrdersByCreatedAt.Query;
using Masterov.Domain.Masterov.Order.GetOrdersByDescription;
using Masterov.Domain.Masterov.Order.GetOrdersByDescription.Query;
using Masterov.Domain.Masterov.Order.GetOrdersByStatus;
using Masterov.Domain.Masterov.Order.GetOrdersByStatus.Query;
using Masterov.Domain.Masterov.Order.GetProductComponentByOrderId;
using Masterov.Domain.Masterov.Order.GetProductComponentByOrderId.Query;
using Masterov.Domain.Masterov.Order.UpdateOrder;
using Masterov.Domain.Masterov.Order.UpdateOrder.Command;
using Masterov.Domain.Masterov.Order.UpdateOrderStatus;
using Masterov.Domain.Masterov.Order.UpdateOrderStatus.Command;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId.Query;
using Masterov.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Ордера (заказы)
/// </summary>
/// <param name="mapper"></param>
[ApiController]
[Route("api/orders")]
public class OrderController(IMapper mapper) : ControllerBase
{
    // TODO если статус Canceled тогда все компоненты должны вернуться на склад с которого взяли и соответсвенно?

    /// <summary>
    /// Получить все заказы
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о всех заказах (ордерах)</returns>
    [HttpGet("getOrders")]
    [ProducesResponseType(200, Type = typeof(OrderRequest[]))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetOrders(
        [FromServices] IGetOrdersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders = await useCase.Execute(cancellationToken);
        return Ok(orders.Select(mapper.Map<OrderRequest>));
    }

    /// <summary>
    /// Получить заказ по Id
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказе</returns>
    [HttpGet("getOrderById/{orderId:guid}")]
    [ProducesResponseType(200, Type = typeof(OrderRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetOrderById(
        [FromRoute] Guid orderId,
        [FromServices] IGetOrderByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var order = await useCase.Execute(new GetOrderByIdQuery(orderId), cancellationToken);
        return Ok(mapper.Map<OrderRequest>(order));
    }

    /// <summary>
    /// Получить список ордеров (заказов) по дате создания
    /// </summary>
    /// <param name="request">Дата создания ордера (заказа)</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказах (ордерах)</returns>
    [HttpGet("getProductionOrdersByCreatedAt")]
    [ProducesResponseType(200, Type = typeof(OrderRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductionOrdersByCreatedAt(
        [FromQuery] GetOrderByCreatedAtRequest request,
        [FromServices] IGetOrdersByCreatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders =
            await useCase.Execute(new GetOrdersByCreatedAtQuery(request.CreatedAt), cancellationToken);
        return Ok(orders?.Select(mapper.Map<OrderRequest>) ?? Array.Empty<OrderRequest>());
    }

    /// <summary>
    /// Получить список ордеров (заказов) по дате закрытия
    /// </summary>
    /// <param name="request">Дата закрытия ордера (заказа)</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказах (ордерах)</returns>
    [HttpGet("getProductionOrdersByCompletedAt")]
    [ProducesResponseType(200, Type = typeof(OrderRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductionOrdersByCompletedAt(
        [FromQuery] GetOrderByCompletedAtRequest request,
        [FromServices] IGetOrdersByCompletedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders = await useCase.Execute(new GetOrdersByCompletedAtQuery(request.CompletedAt),
            cancellationToken);
        return Ok(orders?.Select(mapper.Map<OrderRequest>) ?? Array.Empty<OrderRequest>());
    }

    /// <summary>
    /// Получить список ордеров (заказов) по описанию
    /// </summary>
    /// <param name="request">Описание ордера (заказа)</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказах (ордерах)</returns>
    [HttpGet("getProductionOrdersByDescription")]
    [ProducesResponseType(200, Type = typeof(OrderRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductionOrdersByDescription(
        [FromQuery] GetOrderByDescriptionRequest request,
        [FromServices] IGetOrdersByDescriptionUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders = await useCase.Execute(new GetOrdersByDescriptionQuery(request.Description),
            cancellationToken);
        return Ok(orders?.Select(mapper.Map<OrderRequest>) ?? Array.Empty<OrderRequest>());
    }

    /// <summary>
    /// Получить список ордеров (заказов) по статусу
    /// </summary>
    /// <param name="request">Статус ордера (заказа)</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказах (ордерах)</returns>
    [HttpGet("getProductionOrdersByStatus")]
    [ProducesResponseType(200, Type = typeof(OrderRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductionOrdersByStatus(
        [FromQuery] GetOrderByStatusRequest request,
        [FromServices] IGetOrdersByStatusUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders =
            await useCase.Execute(
                new GetOrdersByStatusQuery(EnumTypeHelper.FromExtensionProductionOrderStatus(request.Status)),
                cancellationToken);
        return Ok(orders?.Select(mapper.Map<OrderRequest>) ?? Array.Empty<OrderRequest>());
    }

    /// <summary>
    /// Получить готовое мебельное изделие у ордера
    /// </summary>
    /// <param name="request">Id Ордера (заказа)</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Готовое мебельное изделие</returns>
    [HttpGet("getFinishedProductByOrderId")]
    [ProducesResponseType(200, Type = typeof(FinishedProductRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetFinishedProductByOrderId(
        [FromQuery] GetFinishedProductByOrderIdRequest request,
        [FromServices] IGetFinishedProductByOrderIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var product = await useCase.Execute(new GetFinishedProductByOrderIdQuery(request.OrderId), cancellationToken);
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
    [ProducesResponseType(201, Type = typeof(OrderRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> AddProductionOrder(
        [FromForm] AddOrderRequest request,
        [FromServices] IAddOrderUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productionOrder = await useCase.Execute(
            new AddOrderCommand(request.FinishedProductId, request.Description, request.CustomerId,
                request.CustomerName, request.CustomerPhone, request.CustomerEmail), cancellationToken);

        return CreatedAtAction(nameof(GetOrderById),
            new { orderId = productionOrder.OrderId },
            mapper.Map<OrderRequest>(productionOrder));
    }

    /// <summary>
    /// Обновить статус у ордера (заказа)
    /// </summary>
    /// <param name="request">Данные для обновления статуса заказа</param>
    /// <param name="useCase">Сценарий обновления статуса заказа</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPatch("updateProductionOrderStatus")]
    [ProducesResponseType(201, Type = typeof(OrderRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    //[Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdateProductionOrderStatus(
        [FromForm] UpdateOrderStatusRequest request,
        [FromServices] IUpdateOrderStatusUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productionOrder = await useCase.Execute(
            new UpdateOrderStatusCommand(request.OrderId,
                EnumTypeHelper.FromExtensionProductionOrderStatus(request.Status)), cancellationToken);

        return CreatedAtAction(nameof(GetOrderById),
            new { orderId = productionOrder.OrderId },
            mapper.Map<OrderRequest>(productionOrder));
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
        [FromForm] UpdateOrderRequest request,
        [FromServices] IUpdateOrderUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productionOrder = await useCase.Execute(
            new UpdateOrderCommand(
                request.OrderId, 
                request.CreatedAt,
                EnumTypeHelper.FromExtensionProductionOrderStatus(request.Status), 
                request.Description,
                request.CustomerId),
            cancellationToken);

        return CreatedAtAction(nameof(GetOrderById),
            new { orderId = productionOrder.OrderId },
            mapper.Map<OrderRequest>(productionOrder));
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
        [FromServices] IDeleteOrderUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(new DeleteOrderCommand(productionOrderId), cancellationToken);
        return NoContent();
    }
}