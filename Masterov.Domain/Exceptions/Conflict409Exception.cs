namespace Masterov.Domain.Exceptions;

public class Conflict409Exception(string name)
    : DomainException(ErrorCode.StatusCode409, $"{name}")
{
    public string Name = name;
}