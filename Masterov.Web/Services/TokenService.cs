using System.Text.Json;

namespace Masterov.Web.Services;

public class TokenService
{
    public bool IsTokenValid(string token)
    {
        try
        {
            var parts = token.Split('.');
            if (parts.Length != 3) return false;

            var payload = parts[1];
            var paddedPayload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
            var bytes = Convert.FromBase64String(paddedPayload);
            var json = System.Text.Encoding.UTF8.GetString(bytes);
            var doc = JsonDocument.Parse(json);

            if (!doc.RootElement.TryGetProperty("exp", out var exp)) return false;

            var expTime = DateTimeOffset.FromUnixTimeSeconds(exp.GetInt64());
            return expTime > DateTimeOffset.UtcNow;
        }
        catch
        {
            return false;
        }
    }
}