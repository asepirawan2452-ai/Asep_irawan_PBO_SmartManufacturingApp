using Microsoft.EntityFrameworkCore;
using SmartManufacturingApp.Models;

namespace SmartManufacturingApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Machine> Machines { get; set; }

    public DbSet<OperatorEmployee> Operators { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductionOrder> ProductionOrders { get; set; }

    public DbSet<ProductionOrderDetail> ProductionOrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductionOrder>()
            .HasOne(x => x.Machine)
            .WithMany()
            .HasForeignKey(x => x.MachineId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProductionOrder>()
            .HasOne(x => x.OperatorEmployee)
            .WithMany()
            .HasForeignKey(x => x.OperatorEmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProductionOrderDetail>()
            .HasOne(x => x.ProductionOrder)
            .WithMany(x => x.Details)
            .HasForeignKey(x => x.ProductionOrderId);

        modelBuilder.Entity<ProductionOrderDetail>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
