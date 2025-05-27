namespace Masterov.Domain.Exceptions;

public class NotFoundByIdException(Guid id, string name)
    : DomainException(ErrorCode.StatusCode410, $"{name} с таким id : \"{id}\" - отсутсвует")
{
    public Guid Id { get; } = id;
}