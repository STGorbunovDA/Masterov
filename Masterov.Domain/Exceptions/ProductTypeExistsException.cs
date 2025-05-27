namespace Masterov.Domain.Exceptions;

public class ProductTypeExistsException()
    : DomainException(ErrorCode.StatusCode409, $"Такой тип изделия уже существует.")
{
    
}