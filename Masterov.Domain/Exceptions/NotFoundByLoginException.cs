namespace Masterov.Domain.Exceptions;

public class NotFoundByLoginException(string login)
    : DomainException(ErrorCode.StatusCode401, $"\"{login}\" - не зарегистрирован")
{
    public string Login { get; } = login;
}