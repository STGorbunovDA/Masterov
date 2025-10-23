namespace Masterov.Domain.Exceptions;

public class InsufficientQuantityException : Conflict409Exception
{
    public int Available { get; }
    public int Requested { get; }
    
    public InsufficientQuantityException(int available, int requested) 
        : base($"Недостаточно компонентов на складе. Доступно: {available}, Запрошено: {requested}")
    {
        Available = available;
        Requested = requested;
    }
}