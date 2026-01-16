using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage;

public class MasterovDbContext : DbContext
{
    public MasterovDbContext(DbContextOptions<MasterovDbContext> options) : base(options) { }
    
    public DbSet<FinishedProduct> FinishedProducts { get; set; }
    public DbSet<UsedComponent> UsedComponents { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Supply> Supplies { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsedComponent>()
            .Property(p => p.WarehouseId)
            .HasColumnType("char(36)")
            .UseCollation("ascii_general_ci");

        modelBuilder.Entity<Warehouse>()
            .Property(w => w.WarehouseId)
            .HasColumnType("char(36)")
            .UseCollation("ascii_general_ci");
        
        // Customer имеет UserId как внешний ключ, который может быть null.
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.User)
            .WithOne(u => u.Customer)
            .HasForeignKey<Customer>(c => c.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}