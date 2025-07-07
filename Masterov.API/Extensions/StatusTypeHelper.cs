using Masterov.Domain.Extension;

namespace Masterov.API.Extensions;

public static class StatusTypeHelper
{
    public static ProductionOrderStatus FromExtensionProductionOrderStatus(string extension) => extension.ToLower() switch
    {
        "draft" => ProductionOrderStatus.Draft,
        "partial" => ProductionOrderStatus.Partial,
        "inprogress" => ProductionOrderStatus.InProgress,
        "completed" => ProductionOrderStatus.Completed,
        "canceled" => ProductionOrderStatus.Canceled,
        _ => ProductionOrderStatus.Unknown
    };
    
    public static PaymentMethod FromExtensionPaymentMethod(string extension) => extension.ToLower() switch
    {
        "card" => PaymentMethod.Card,
        "cash" => PaymentMethod.Cash,
        "bankTransfer" => PaymentMethod.BankTransfer,
        _ => PaymentMethod.Unknown
    };
}