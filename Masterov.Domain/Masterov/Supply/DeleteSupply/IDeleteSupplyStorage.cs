namespace Masterov.Domain.Masterov.Supply.DeleteSupply;

public interface IDeleteSupplyStorage
{
    Task<bool> DeleteSupply(Guid supplyId, CancellationToken cancellationToken);
}