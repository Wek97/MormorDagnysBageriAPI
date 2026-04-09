using Microsoft.EntityFrameworkCore;
using MormorDagnysBageri.Entities;


namespace MormorDagnysBageri.Data;

public class MormorDagnysContext(DbContextOptions<MormorDagnysContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SupplierProduct> SupplierProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupplierProduct>()
            .HasKey(sp => new { sp.SupplierId, sp.ProductId});

        base.OnModelCreating(modelBuilder);
    }

}
