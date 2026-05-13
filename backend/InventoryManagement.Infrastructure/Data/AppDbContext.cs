using InventoryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Data;

// DbContext centrinė EF Core klasė, kuri atstovauja duomenų bazę 
public class AppDbContext : DbContext
{
    // Dependency injection naudojamas, kad galėtume konfigūruoti DbContext iš išorės 
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User su Item ryšys
        modelBuilder.Entity<InventoryItem>()
            .HasOne(i => i.User)
            .WithMany(u => u.Items)
            .HasForeignKey(i => i.UserId);

        // Soft delete apsauga
        modelBuilder.Entity<InventoryItem>()
            .HasQueryFilter(i => i.IsActive);
    }
}