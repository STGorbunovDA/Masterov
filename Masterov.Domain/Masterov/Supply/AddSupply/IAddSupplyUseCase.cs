using Masterov.Domain.Masterov.Supply.AddSupply.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.AddSupply;

public interface IAddSupplyUseCase
{
    Task<SupplyDomain> Execute(AddSupplyCommand addSupplyCommand, CancellationToken cancellationToken);
}