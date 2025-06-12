using Masterov.Domain.Extension;

namespace Masterov.API.Extensions;

public static class StatusTypeHelper
{
    public static ProductionOrderStatus FromExtension(string extension) => extension.ToLower() switch
    {
        "draft" => ProductionOrderStatus.Draft,
        "inProgress" => ProductionOrderStatus.InProgress,
        "completed" => ProductionOrderStatus.Completed,
        "canceled" => ProductionOrderStatus.Canceled,
        _ => ProductionOrderStatus.Unknown
    };
}