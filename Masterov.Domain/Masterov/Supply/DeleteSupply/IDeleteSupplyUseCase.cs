using Masterov.Domain.Masterov.Supply.DeleteSupply.Command;

namespace Masterov.Domain.Masterov.Supply.DeleteSupply;

public interface IDeleteSupplyUseCase
{
    Task<bool> Execute(DeleteSupplyCommand deleteSupplyCommand, CancellationToken cancellationToken);
}