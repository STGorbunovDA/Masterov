namespace Masterov.Domain.Exceptions;

public class FinishedProductExistsException(string name)
    : DomainException(ErrorCode.StatusCode409, $"Готовое мебельное изделие с текущем названием: {name} уже существует")
{
    public string Name = name;
}