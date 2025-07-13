namespace Masterov.Domain.Exceptions;

public class Conflict422Exception(string name)
    : DomainException(ErrorCode.StatusCode422, $"{name}")
{
    public string Name = name;
}