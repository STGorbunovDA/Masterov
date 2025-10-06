namespace Masterov.Domain.Exceptions;

public class NotFoundByLoginException(string login)
    : DomainException(ErrorCode.StatusCode404, $"\"{login}\" - не найден")
{
    public string Login { get; } = login;
}