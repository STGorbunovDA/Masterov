namespace Masterov.Domain.Exceptions;

public class CustomerExistsException : DomainException
{
    public CustomerExistsException(string name, string? email, string? phone) 
        : base(ErrorCode.StatusCode409, FormatMessage(name, email, phone))
    {
        Name = name;
        Email = email;
        Phone = phone;
    }

    public string Name { get; }
    public string? Email { get; }
    public string? Phone { get; }

    private static string FormatMessage(string name, string? email, string? phone)
    {
        var emailInfo = email is not null 
            ? $"({(string.IsNullOrWhiteSpace(email) ? "email: empty" : email)})" 
            : "email: null";
            
        var phoneInfo = phone is not null 
            ? $"({(string.IsNullOrWhiteSpace(phone) ? "phone: empty" : phone)})" 
            : "phone: null";

        return $"Заказчик '{name}' {emailInfo} {phoneInfo} уже существует.";
    }
}