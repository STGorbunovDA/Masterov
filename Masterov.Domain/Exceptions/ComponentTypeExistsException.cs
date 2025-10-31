namespace Masterov.Domain.Exceptions;

public class ComponentTypeExistsException(string name)
    : DomainException(ErrorCode.StatusCode409, $"Тип компонента с текущем названием: {name}  уже существует.")
{
    public string Name = name;
}