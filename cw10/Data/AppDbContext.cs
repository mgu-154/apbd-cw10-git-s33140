using cw10.Entities;
using Microsoft.EntityFrameworkCore;

namespace cw10.Data;

public class AppDbContext : DbContext
{
    protected AppDbContext()
    {
    }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {}
    
    public DbSet<Pc> Pcs { get; set; }
    public DbSet<PcComponent> PcComponents { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pc>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Name).HasMaxLength(50);
            e.Property(p => p.Weight).HasMaxLength(5);
            
            e.ToTable("PCs");
        });

        modelBuilder.Entity<PcComponent>(e =>
        {
            e.HasKey(p => new { p.PcId, p.ComponentCode });
            e.Property(p => p.ComponentCode).HasMaxLength(10);
            
            e.HasOne(p => p.Pc)
                .WithMany(x => x.PcComponent)
                .HasForeignKey(p => p.PcId)
                .OnDelete(DeleteBehavior.Cascade);
            
            e.HasOne(p => p.Component)
                .WithMany(x => x.PcComponent)
                .HasForeignKey(p => p.ComponentCode)
                .OnDelete(DeleteBehavior.Cascade);
            
            e.ToTable("PCComponents");
        });

        modelBuilder.Entity<Component>(e =>
        {
            e.HasKey(c => c.Code);
            e.Property(c => c.Code).HasMaxLength(10);
            e.Property(c => c.Name).HasMaxLength(300);
            
            e.HasOne(c => c.ComponentManufacturer)
                .WithMany(x => x.Component)
                .HasForeignKey(c => c.ComponentManufacturerId)
                .OnDelete(DeleteBehavior.Cascade);
            
            e.HasOne(c => c.ComponentType)
                .WithMany(x => x.Component)
                .HasForeignKey(c => c.ComponentTypeId)
                .OnDelete(DeleteBehavior.Cascade);
            
            e.ToTable("Components");
        });

        modelBuilder.Entity<ComponentManufacturer>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Abbreviation).HasMaxLength(30);
            e.Property(c => c.FullName).HasMaxLength(300);
            
            e.ToTable("ComponentManufacturers");
        });

        modelBuilder.Entity<ComponentType>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Abbreviation).HasMaxLength(30);
            e.Property(c => c.Name).HasMaxLength(150);
            
            e.ToTable("ComponentTypes");
        });
        
        base.OnModelCreating(modelBuilder);
    }
}