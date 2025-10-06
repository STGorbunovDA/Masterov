namespace Masterov.Domain.Exceptions;

public class UserUpdateException()
    : DomainException(ErrorCode.StatusCode409, $"Ошибка обновления пользователя")
{
    
}