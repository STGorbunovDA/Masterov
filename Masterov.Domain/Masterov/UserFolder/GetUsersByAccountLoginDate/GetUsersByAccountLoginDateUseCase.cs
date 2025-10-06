using FluentValidation;
using Masterov.Domain.Masterov.UserFolder.GetUsersByAccountLoginDate.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUsersByAccountLoginDate;

public class GetUsersByAccountLoginDateUseCase(IValidator<GetUsersByAccountLoginDateQuery> validator,
    IGetUsersByAccountLoginDateStorage storage) : IGetUsersByAccountLoginDateUseCase
{
    public async Task<IEnumerable<UserDomain>?> Execute(GetUsersByAccountLoginDateQuery byAccountLoginDateQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(byAccountLoginDateQuery, cancellationToken);
        
        return await storage.GetUsersByAccountLoginDate(byAccountLoginDateQuery.AccountLoginDate, cancellationToken);
    }
}