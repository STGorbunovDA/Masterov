namespace Masterov.Domain.Exceptions;

public class NotFoundByPhoneException(string phone)
    : DomainException(ErrorCode.StatusCode404, $"Заказчик с таким телефоном : \"{phone}\" - отсутсвует")
{
    public string Phone { get; } = phone;
}