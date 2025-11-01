using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models.Customer;
using Masterov.API.Models.FinishedProduct;
using Masterov.API.Models.Order;
using Masterov.API.Models.Payment;
using Masterov.API.Models.UsedComponent;
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
using Masterov.Domain.Masterov.Order.GetOrdersByUpdatedAt;
using Masterov.Domain.Masterov.Order.GetOrdersByUpdatedAt.Query;
using Masterov.Domain.Masterov.Order.GetUsedComponentsByOrderId;
using Masterov.Domain.Masterov.Order.GetUsedComponentsByOrderId.Query;
using Masterov.Domain.Masterov.Order.UpdateOrder;
using Masterov.Domain.Masterov.Order.UpdateOrder.Command;
using Masterov.Domain.Masterov.Order.UpdateOrderStatus;
using Masterov.Domain.Masterov.Order.UpdateOrderStatus.Command;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Заказы
/// </summary>
/// <param name="mapper"></param>
[ApiController]
[Route("api/orders")]
public class OrderController(IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить все заказы
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о всех заказах (ордерах)</returns>
    [HttpGet("getOrders")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OrderResponse>))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetOrders(
        [FromServices] IGetOrdersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders = await useCase.Execute(cancellationToken);
        return Ok(orders?.Select(mapper.Map<OrderResponse>) ?? Array.Empty<OrderResponse>());
    }

    /// <summary>
    /// Получить заказ по Id
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказе</returns>
    [HttpGet("getOrderById/{orderId:guid}")]
    [ProducesResponseType(200, Type = typeof(OrderResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetOrderById(
        [FromRoute] Guid orderId,
        [FromServices] IGetOrderByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var order = await useCase.Execute(new GetOrderByIdQuery(orderId), cancellationToken);
        return Ok(mapper.Map<OrderResponse>(order));
    }

    /// <summary>
    /// Получить список заказов по дате создания
    /// </summary>
    /// <param name="request">Дата создания заказа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказах</returns>
    [HttpGet("getOrdersByCreatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OrderResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetOrdersByCreatedAt(
        [FromQuery] GetOrdersByCreatedAtRequest request,
        [FromServices] IGetOrdersByCreatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders =
            await useCase.Execute(new GetOrdersByCreatedAtQuery(request.CreatedAt.ToDateTime()), cancellationToken);
        return Ok(orders?.Select(mapper.Map<OrderResponse>) ?? Array.Empty<OrderResponse>());
    }
    
    /// <summary>
    /// Получить список заказов по дате обновления
    /// </summary>
    /// <param name="request">Дата обновления заказа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказах</returns>
    [HttpGet("getOrdersByUpdatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OrderResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetOrdersByUpdatedAt(
        [FromQuery] GetOrdersByUpdatedAtRequest request,
        [FromServices] IGetOrdersByUpdatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders =
            await useCase.Execute(new GetOrdersByUpdatedAtQuery(request.UpdatedAt.ToDateTime()), cancellationToken);
        return Ok(orders?.Select(mapper.Map<OrderResponse>) ?? Array.Empty<OrderResponse>());
    }

    /// <summary>
    /// Получить список заказов по дате закрытия
    /// </summary>
    /// <param name="request">Дата закрытия заказа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказах</returns>
    [HttpGet("getOrdersByCompletedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OrderResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetOrdersByCompletedAt(
        [FromQuery] GetOrdersByCompletedAtRequest request,
        [FromServices] IGetOrdersByCompletedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders = await useCase.Execute(new GetOrdersByCompletedAtQuery(request.CompletedAt.ToDateTime()),
            cancellationToken);
        return Ok(orders?.Select(mapper.Map<OrderResponse>) ?? Array.Empty<OrderResponse>());
    }

    /// <summary>
    /// Получить список ордеров заказов по описанию
    /// </summary>
    /// <param name="request">Описание заказа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказах</returns>
    [HttpGet("getOrdersByDescription")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OrderResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetOrdersByDescription(
        [FromQuery] GetOrdersByDescriptionRequest request,
        [FromServices] IGetOrdersByDescriptionUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders = await useCase.Execute(new GetOrdersByDescriptionQuery(request.Description), cancellationToken);
        return Ok(orders?.Select(mapper.Map<OrderResponse>) ?? Array.Empty<OrderResponse>());
    }

    /// <summary>
    /// Получить список заказов по статусу
    /// </summary>
    /// <param name="request">Статус заказа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказах</returns>
    [HttpGet("getOrdersByStatus")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OrderResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetOrdersByStatus(
        [FromQuery] GetOrdersByStatusRequest request,
        [FromServices] IGetOrdersByStatusUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders = await useCase.Execute(new GetOrdersByStatusQuery(EnumTypeHelper.FromExtensionOrderStatus(request.Status)), cancellationToken);
        return Ok(orders?.Select(mapper.Map<OrderResponse>) ?? Array.Empty<OrderResponse>());
    }

    /// <summary>
    /// Получить готовое мебельное изделие по идентификатору заказа
    /// </summary>
    /// <param name="request">Идентификатор заказа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Готовое мебельное изделие</returns>
    [HttpGet("getFinishedProductByOrderId")]
    [ProducesResponseType(200, Type = typeof(FinishedProductResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetFinishedProductByOrderId(
        [FromQuery] GetFinishedProductByOrderIdRequest request,
        [FromServices] IGetFinishedProductByOrderIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var product = await useCase.Execute(new GetFinishedProductByOrderIdQuery(request.OrderId), cancellationToken);
        return Ok(mapper.Map<FinishedProductResponse>(product));
    }

    /// <summary>
    /// Получить используемые компоненты заказа по идентификатору заказа
    /// </summary>
    /// <param name="request">Идентификатор заказа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Используемые компоненты</returns>
    [HttpGet("getUsedComponentsByOrderId")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UsedComponentResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetUsedComponentsByOrderId(
        [FromQuery] GetUsedComponentsByOrderIdRequest request,
        [FromServices] IGetUsedComponentsByOrderIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var usedComponents = await useCase.Execute(new GetUsedComponentsByOrderIdQuery(request.OrderId), cancellationToken);
        return Ok(mapper.Map<IEnumerable<UsedComponentResponse>>(usedComponents));
    }

    /// <summary>
    /// Получить заказчика по идентификатору заказа
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказчике</returns>
    [HttpGet("getCustomerByOrderId/{orderId:guid}")]
    [ProducesResponseType(200, Type = typeof(CustomerNoOrdersResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetCustomerByOrderId(
        [FromRoute] Guid orderId,
        [FromServices] IGetCustomerByOrderIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var customer = await useCase.Execute(new GetCustomerByOrderIdQuery(orderId), cancellationToken);
        return Ok(mapper.Map<CustomerNoOrdersResponse>(customer));
    }

    /// <summary>
    /// Получить платежи по идентификатору заказа
    /// </summary>
    /// <param name="request">Идентификатор заказа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о платежах заказа</returns>
    [HttpGet("getPaymentsByOrderId")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<PaymentNewResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize]
    public async Task<IActionResult> GetPaymentsByOrderId(
        [FromQuery] GetPaymentsByOrderIdRequest request,
        [FromServices] IGetPaymentsByOrderIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var payments =
            await useCase.Execute(new GetPaymentsByOrderIdQuery(request.OrderId), cancellationToken);
        return Ok(payments?.Select(mapper.Map<PaymentNewResponse>) ?? Array.Empty<PaymentNewResponse>());
    }

    /// <summary>
    /// Добавить заказ
    /// </summary>
    /// <param name="request">Данные для добавления заказа</param>
    /// <param name="useCase">Сценарий добавления ордера</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPost("addOrder")]
    [ProducesResponseType(201, Type = typeof(OrderResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> AddOrder(
        [FromBody] AddOrderRequest request,
        [FromServices] IAddOrderUseCase useCase,
        CancellationToken cancellationToken)
    {
        var order = await useCase.Execute(
            new AddOrderCommand(request.FinishedProductId, request.Description, request.CustomerId), cancellationToken);
 
        return CreatedAtAction(nameof(GetOrderById),
            new { orderId = order.OrderId },
            mapper.Map<OrderResponse>(order));
    }

    /// <summary>
    /// Обновить статус заказа
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="request">Данные для обновления статуса заказа</param>
    /// <param name="useCase">Сценарий обновления статуса заказа</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPatch("updateOrderStatus/{orderId:guid}")]
    [ProducesResponseType(201, Type = typeof(OrderResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdateOrderStatus(
        [FromRoute] Guid orderId,
        [FromForm] UpdateOrderStatusRequest request,
        [FromServices] IUpdateOrderStatusUseCase useCase,
        CancellationToken cancellationToken)
    {
        var order = await useCase.Execute(new UpdateOrderStatusCommand(orderId,
                EnumTypeHelper.FromExtensionOrderStatus(request.Status)), cancellationToken);

        return CreatedAtAction(nameof(GetOrderById),
            new { orderId = order.OrderId },
            mapper.Map<OrderResponse>(order));
    }

    /// <summary>
    /// Обновить заказ
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="request">Данные для обновления заказа</param>
    /// <param name="useCase">Сценарий обновления заказа</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPatch("updateOrder/{orderId:guid}")]
    [ProducesResponseType(200, Type = typeof(OrderResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(OrderResponse))]
    public async Task<IActionResult> UpdateOrder(
        [FromRoute] Guid orderId,
        [FromForm] UpdateOrderRequest request,
        [FromServices] IUpdateOrderUseCase useCase,
        CancellationToken cancellationToken)
    {
        var order = await useCase.Execute(
            new UpdateOrderCommand(
                orderId, 
                request.CreatedAt.ToDateTime(),
                request.CompletedAt.ToDateTime(),
                EnumTypeHelper.FromExtensionOrderStatus(request.Status), 
                request.Description,
                request.FinishedProductId,
                request.CustomerId),
            cancellationToken);

        return CreatedAtAction(nameof(GetOrderById),
            new { orderId = order.OrderId },
            mapper.Map<OrderResponse>(order));
    }

    /// <summary>
    /// Удаление заказа по указанному Id.
    /// </summary>
    /// <param name="orderId">Идентификатор заказчика</param>
    /// <param name="useCase">Сценарий удаления заказа</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Ответ с кодом 204, если заказ был успешно удален</returns>
    [HttpDelete("deleteOrder/{orderId:guid}")]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    [ProducesResponseType(204, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [ProducesResponseType(409, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteOrder(
        Guid orderId,
        [FromServices] IDeleteOrderUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(new DeleteOrderCommand(orderId), cancellationToken);
        return NoContent();
    }
}