namespace Masterov.Domain.Exceptions;

public class UserExistsException()
    : DomainException(ErrorCode.StatusCode409, $"Такой пользователь уже существует.")
{
    
}