using System.Globalization;

namespace Masterov.API.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Преобразует строку в DateTime по формату dd.MM.yyyy HH:mm:ss.
        /// Если строка некорректна — выбрасывает исключение FormatException.
        /// </summary>
        public static DateTime? ToDateTime(this string? dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return null;

            // Пробуем сначала полный формат с временем
            if (DateTime.TryParseExact(dateString, "dd.MM.yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }

            // Если не получилось, пробуем формат без времени (для обратной совместимости)
            if (DateTime.TryParseExact(dateString, "dd.MM.yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return date;
            }

            throw new FormatException($"Некорректный формат даты: {dateString}. Ожидается формат dd.MM.yyyy HH:mm:ss или dd.MM.yyyy");
        }

        /// <summary>
        /// Преобразует строку в DateTime с использованием указанного формата.
        /// Если строка некорректна — выбрасывает исключение FormatException.
        /// </summary>
        public static DateTime? ToDateTime(this string? dateString, string format)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return null;

            if (DateTime.TryParseExact(dateString, format,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }

            throw new FormatException($"Некорректный формат даты: {dateString}. Ожидается формат: {format}");
        }
    }
}