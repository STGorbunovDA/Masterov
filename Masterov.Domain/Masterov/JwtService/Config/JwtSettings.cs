namespace Masterov.Domain.Masterov.JwtService.Config;

public class JwtSettings
{
    public string SecretKey { get; set; } = string.Empty;
    public string IssuerKey { get; set; } = string.Empty;
    public string AudienceKey { get; set; } = string.Empty;
    public int ExpireHours { get; set; } = 24;
}