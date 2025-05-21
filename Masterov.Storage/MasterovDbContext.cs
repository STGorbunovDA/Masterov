using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage;

public class MasterovDbContext : DbContext
{
    public MasterovDbContext(DbContextOptions<MasterovDbContext> options) : base(options) { }
    
    public DbSet<Product> Products { get; set; }
}