using System.ComponentModel.DataAnnotations;

namespace Masterov.API.Models;

public class UploadFileRequest
{
    [Required]
    public IFormFile File { get; set; } = null!;
}