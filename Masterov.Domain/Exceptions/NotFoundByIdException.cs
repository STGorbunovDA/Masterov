namespace Masterov.Domain.Exceptions;

public class NotFoundByIdException(Guid id, string name)
    : DomainException(ErrorCode.StatusCode404, $"{name} с таким id : \"{id}\" - отсутсвует")
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
}