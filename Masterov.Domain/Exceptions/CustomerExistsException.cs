namespace Masterov.Domain.Exceptions;

public class CustomerExistsException(string name, string email, string phone)
    : DomainException(ErrorCode.StatusCode409, $"Заказчик: {name}, {email}, {phone} уже существует")
{
    public string Name = name;
    public string Email = email;
    public string Phone = phone;
}