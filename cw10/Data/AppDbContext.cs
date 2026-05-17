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

        modelBuilder.Entity<Pc>().HasData(new List<Pc>()
        {
            new Pc()
            {
                Id = 1, Name = "Pc", Weight = 5.5f, Warranty = 2, CreatedAt = new DateTime(2025, 10, 5), Stock = 5
            },
            new Pc()
            {
                Id = 2, Name = "Notebook", Weight = 2.0f, Warranty = 1, CreatedAt = new DateTime(2026, 2, 28), Stock = 2
            },
            new Pc()
            {
                Id = 3, Name = "Ultrabook", Weight = 3.6f, Warranty = 1, CreatedAt = new DateTime(2020, 7, 10),
                Stock = 15
            },
        });

        modelBuilder.Entity<ComponentManufacturer>().HasData(new List<ComponentManufacturer>()
        {
            new ComponentManufacturer()
            {
                Id = 1, Abbreviation = "Asus", FullName = "ASUSTeK Computer Inc.",
                FoundationDate = new DateTime(1989, 4, 2)
            },
            new ComponentManufacturer()
            {
                Id = 2, Abbreviation = "Razer", FullName = "Razer Inc.",
                FoundationDate = new DateTime(1998, 3, 14)
            },
            new ComponentManufacturer()
            {
                Id = 3, Abbreviation = "MSI", FullName = "Micro-Star International Co., Ltd",
                FoundationDate = new DateTime(1986, 8, 4)
            },
            new ComponentManufacturer()
            {
                Id = 4, Abbreviation = "Intel", FullName = "Intel Corporation",
                FoundationDate = new DateTime(1968, 7, 18)
            }
        });

        modelBuilder.Entity<ComponentType>().HasData(new List<ComponentType>()
        {
            new ComponentType()
            {
                Id = 1, Abbreviation = "CPU", Name = "Central Processing Unit"
            },
            new ComponentType()
            {
                Id = 2, Abbreviation = "GPU", Name = "Graphics Processing Unit"
            },
            new ComponentType()
            {
                Id = 3, Abbreviation = "RAM", Name = "Random Access Memory"
            },
        });

        modelBuilder.Entity<Component>().HasData(new List<Component>()
        {
            new Component()
            {
                Code = 'A', Name = "Intel Core i9", Description = "10920X, 3.5GHz, Socket 2066, 12 cores, 20MB cache",
                ComponentManufacturerId = 4, ComponentTypeId = 1
            },
            new Component()
            {
                Code = 'B', Name = "Asus Radeon RX 9070", Description = "XT Prime 16GB OC", ComponentManufacturerId = 1,
                ComponentTypeId = 2
            },
            new Component()
            {
                Code = 'C', Name = "MSI Trident 3", Description = "32GB DDR4, 2666MHz, 9th gen, 260-pin SO-DIMM",
                ComponentManufacturerId = 3, ComponentTypeId = 3
            },
        });

        modelBuilder.Entity<PcComponent>().HasData(new List<PcComponent>()
        {
            new PcComponent() { PcId = 2, ComponentCode = 'C', Amount = 2 },
            new PcComponent() { PcId = 2, ComponentCode = 'A', Amount = 1 },
            new PcComponent() { PcId = 3, ComponentCode = 'A', Amount = 1 },
            new PcComponent() { PcId = 1, ComponentCode = 'B', Amount = 1 },
        });
        
        base.OnModelCreating(modelBuilder);
    }
}