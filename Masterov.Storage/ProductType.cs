using System.ComponentModel.DataAnnotations;

namespace Masterov.Storage;

public class ProductType
{
    [Key]
    public Guid ProductTypeId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime? UpdatedAt  { get; set; }

    public ICollection<FinishedProduct> Products { get; set; }
}