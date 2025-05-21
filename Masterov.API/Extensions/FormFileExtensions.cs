namespace Masterov.API.Extensions;

public static class FormFileExtensions
{
    public static async Task<byte[]> ToByteArrayAsync(this IFormFile file)
    {
        if (file == null)
            throw new ArgumentNullException(nameof(file));

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}