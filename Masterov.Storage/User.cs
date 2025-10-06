using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Masterov.Domain.Extension;

namespace Masterov.Storage;

public class User
{
    [Key]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Login { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(20)")]
    public UserRole Role { get; set; } = UserRole.RegularUser;
    
    [Required]
    [Column(TypeName = "varchar(100)")]
    public string PasswordHash { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }
}