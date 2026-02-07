namespace Masterov.Domain.Exceptions;

public class ProductTypeExistsException(string name)
    : DomainException(ErrorCode.StatusCode409, $"Тип готового мебельного изделия с текущем названием: {name}  уже существует.")
{
    public string Name = name;
}