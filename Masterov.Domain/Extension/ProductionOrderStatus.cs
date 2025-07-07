namespace Masterov.Domain.Extension;

public enum ProductionOrderStatus
{
    Draft = 0,     // Черновик
    Partial = 1, // В заявке (частичная оплата)
    InProgress = 2, // В работе
    Completed = 3,  // Готово
    Canceled = 4,    // Отменен
    Unknown = 5, // 
}