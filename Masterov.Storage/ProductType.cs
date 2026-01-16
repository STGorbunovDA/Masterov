using System.ComponentModel.DataAnnotations;

namespace Masterov.Storage;

public class ProductType
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    public ICollection<FinishedProduct> Products { get; set; }
}