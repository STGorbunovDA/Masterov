namespace Masterov.Domain.Exceptions;

public class UserInvalidPasswordException()
    : DomainException(ErrorCode.StatusCode401, $"Неверный пароль")
{
    
}