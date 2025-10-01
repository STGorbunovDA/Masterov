namespace Masterov.Domain.Exceptions;

public class NotFoundByNameException(string nameOne, string nameTwo)
    : DomainException(ErrorCode.StatusCode404, $"{nameTwo} с таким названием : \"{nameOne}\" - отсутсвует")
{
    public string NameOne { get; } = nameOne;
}