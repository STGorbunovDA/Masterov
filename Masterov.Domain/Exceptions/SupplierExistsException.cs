namespace Masterov.Domain.Exceptions;

public class SupplierExistsException : DomainException
{
    public SupplierExistsException(string name, string? surname, string? email, string? phone, string? address)
        : base(ErrorCode.StatusCode409, FormatMessage(name, surname, email, phone, address))
    {
        Name = name;
        Surname = surname;
        Email = email;
        Phone = phone;
        Address = address;
    }

    public string Name { get; }
    public string? Surname { get; }
    public string? Email { get; }
    public string? Phone { get; }
    public string? Address { get; }

    private static string FormatMessage(string name, string? surname, string? email, string? phone, string? address)
    {
        var surnameInfo = surname is not null
            ? $"({(string.IsNullOrWhiteSpace(surname) ? "surname: empty" : surname)})"
            : "surname: null";

        var emailInfo = email is not null
            ? $"({(string.IsNullOrWhiteSpace(email) ? "email: empty" : email)})"
            : "email: null";

        var phoneInfo = phone is not null
            ? $"({(string.IsNullOrWhiteSpace(phone) ? "phone: empty" : phone)})"
            : "phone: null";

        var addressInfo = address is not null
            ? $"({(string.IsNullOrWhiteSpace(address) ? "address: empty" : address)})"
            : "address: null";

        return $"Поставщик '{name}' {surnameInfo} {emailInfo} {phoneInfo} {addressInfo} уже существует.";
    }
}