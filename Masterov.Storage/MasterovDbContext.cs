using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage;

public class MasterovDbContext : DbContext
{
    public MasterovDbContext(DbContextOptions<MasterovDbContext> options) : base(options) { }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.ProductTypeId)
            .HasDefaultValue(null);
    }
}