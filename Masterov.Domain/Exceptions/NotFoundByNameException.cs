namespace Masterov.Domain.Exceptions;

public class NotFoundByNameException(string nameOne, string nameTwo)
    : DomainException(ErrorCode.StatusCode410, $"{nameTwo} с таким названием : \"{nameOne}\" - отсутсвует")
{
    
}