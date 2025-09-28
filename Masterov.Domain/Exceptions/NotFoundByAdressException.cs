namespace Masterov.Domain.Exceptions;

public class NotFoundByAddressException(string address)
    : DomainException(ErrorCode.StatusCode410, $"Заказчик с таким адресом : \"{address}\" - отсутсвует")
{
    public string Address { get; } = address;
}