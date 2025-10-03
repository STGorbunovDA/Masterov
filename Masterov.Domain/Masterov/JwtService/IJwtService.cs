using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.JwtService;

public interface IJwtService
{
    string GenerateToken(UserDomain user);
}