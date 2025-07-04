namespace Masterov.Domain.Exceptions;

public class NotFoundByAmountException(decimal amount, string name)
    : DomainException(ErrorCode.StatusCode410, $"{name} с такой ценой : \"{amount}\" - отсутсвует")
{
    public decimal Amount { get; } = amount;
}