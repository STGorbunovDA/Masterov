namespace Masterov.Domain.Exceptions;

public class SupplierExistsException(string name, string address, string phone)
    : DomainException(ErrorCode.StatusCode409, $"Поставщик: {name}, {address}, {phone} уже существует")
{
    public string Name = name;
    public string Address = address;
    public string Phone = phone;
}