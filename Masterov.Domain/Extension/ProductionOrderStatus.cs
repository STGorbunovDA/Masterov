namespace Masterov.Domain.Extension;

public enum ProductionOrderStatus
{
    Draft = 0,     // Черновик
    InProgress = 1, // В работе
    Completed = 2,  // Готово
    Canceled = 3    // Отменен
}