using System.Globalization;

namespace Masterov.Domain.Extension;

internal static class DomainExtension
{
    internal static bool IsImage(byte[] content)
    {
        if (content == null || content.Length < 12)
            return false;

        // Проверка сигнатур популярных форматов изображений
        var signatures = new List<Func<byte[], bool>>
        {
            // JPEG
            bytes => bytes.Take(4).SequenceEqual(new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 }),
        
            // PNG
            bytes => bytes.Take(8).SequenceEqual(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }),
        
            // GIF
            bytes => bytes.Take(4).SequenceEqual(new byte[] { 0x47, 0x49, 0x46, 0x38 }),
        
            // BMP
            bytes => bytes.Take(2).SequenceEqual(new byte[] { 0x42, 0x4D }),
        
            // WEBP: "RIFF....WEBP" (байты 0–3 и 8–11)
            bytes => bytes[0] == 0x52 && bytes[1] == 0x49 && bytes[2] == 0x46 && bytes[3] == 0x46 &&
                     bytes[8] == 0x57 && bytes[9] == 0x45 && bytes[10] == 0x42 && bytes[11] == 0x50
        };

        return signatures.Any(check => check(content));
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
    
    internal static bool BeAValidDate(DateTime? date)
    {
        if (!date.HasValue) return true;
        
        var minDate = new DateTime(1000, 1, 1); 
        var currentDate = DateTime.UtcNow;

        return date.Value >= minDate && date.Value <= currentDate;
    }
}