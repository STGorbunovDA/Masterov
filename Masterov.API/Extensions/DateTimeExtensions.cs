using System.Globalization;

namespace Masterov.API.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Преобразует строку в DateTime по формату dd.MM.yyyy.
        /// Если строка некорректна — выбрасывает исключение FormatException.
        /// </summary>
        public static DateTime? ToDateTime(this string? dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return null;

            if (DateTime.TryParseExact(dateString, "dd.MM.yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }

            throw new FormatException($"Некорректный формат даты: {dateString}. Ожидается формат dd.MM.yyyy");
        }
    }
}