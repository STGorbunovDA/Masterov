using System.Globalization;

namespace Masterov.Domain.Extension;

internal static class DomainExtension
{
    internal static bool IsImage(byte[] content)
    {
        if (content == null || content.Length < 4)
            return false;

        // Проверка сигнатур популярных форматов изображений
        var signatures = new Dictionary<string, byte[]>
        {
            { "JPEG", new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 } },
            { "PNG", new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } },
            { "GIF", new byte[] { 0x47, 0x49, 0x46, 0x38 } },
            { "BMP", new byte[] { 0x42, 0x4D } },
            { "WEBP", new byte[] { 0x52, 0x49, 0x46, 0x46, 0, 0, 0, 0, 0x57, 0x45, 0x42, 0x50 } }
        };

        return signatures.Any(signature => 
            signature.Value.SequenceEqual(content.Take(signature.Value.Length)));
    }
    
    internal static bool HasValidPrecisionAndScale(decimal value, int precision, int scale)
    {
        // Разделяем число на целую и дробную части
        string[] parts = value.ToString(CultureInfo.InvariantCulture).Split('.');
    
        // Проверяем общее количество цифр (precision)
        string digits = parts[0].TrimStart('-') + (parts.Length > 1 ? parts[1] : "");
        if (digits.Length > precision - scale) // precision включает scale, поэтому вычитаем scale
        {
            return false;
        }

        // Проверяем количество знаков после запятой (scale)
        if (parts.Length > 1 && parts[1].Length > scale)
        {
            return false;
        }

        return true;
    }
    
}