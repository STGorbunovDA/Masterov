using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage;

public class MasterovDbContext : DbContext
{
    public MasterovDbContext(DbContextOptions<MasterovDbContext> options) : base(options) { }
    
    public DbSet<FinishedProduct> FinishedProducts { get; set; }
    public DbSet<ProductComponent> ProductComponents { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Supply> Supplies { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductComponent>()
            .Property(p => p.WarehouseId)
            .HasColumnType("char(36)")
            .UseCollation("ascii_general_ci");

        modelBuilder.Entity<Warehouse>()
            .Property(w => w.WarehouseId)
            .HasColumnType("char(36)")
            .UseCollation("ascii_general_ci");
    }
}