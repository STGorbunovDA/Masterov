using Masterov.Domain.Extension;

namespace Masterov.API.Extensions;

public static class EnumTypeHelper
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
        "banktransfer" => PaymentMethod.BankTransfer,
        _ => PaymentMethod.Unknown
    };
    
    public static UserRole FromExtensionRoleMethod(string extension) => extension.ToLower() switch
    {
        "superadmin" => UserRole.SuperAdmin,
        "admin" => UserRole.Admin,
        "manager" => UserRole.Manager,
        "regularuser" => UserRole.RegularUser,
        _ => UserRole.Unknown
    };
}