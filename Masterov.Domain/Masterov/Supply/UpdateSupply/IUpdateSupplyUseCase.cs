using Masterov.Domain.Masterov.Supply.UpdateSupply.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.UpdateSupply;

public interface IUpdateSupplyUseCase
{
    Task<SupplyDomain?> Execute(UpdateSupplyCommand command, CancellationToken cancellationToken);
}