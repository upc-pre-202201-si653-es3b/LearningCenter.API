using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Security.Domain.Models;
using LearningCenter.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Tutorial> Tutorials { get; set; }

    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Categories
        
        builder.Entity<Category>().ToTable("Categories");
        builder.Entity<Category>().HasKey(p => p.Id);
        builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(30);
        
        // Relationships
        builder.Entity<Category>()
            .HasMany(p => p.Tutorials)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);
        
        // Tutorials
        builder.Entity<Tutorial>().ToTable("Tutorials");
        builder.Entity<Tutorial>().HasKey(p => p.Id);
        builder.Entity<Tutorial>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Tutorial>().Property(p => p.Title).IsRequired().HasMaxLength(50);
        builder.Entity<Tutorial>().Property(p => p.Description).HasMaxLength(120);
        
        
        // Users
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p => p.Username).IsRequired().HasMaxLength(30);
        builder.Entity<User>().Property(p => p.FirstName).IsRequired();
        builder.Entity<User>().Property(p => p.LastName).IsRequired();
        
        // Apply Naming Conventions
        builder.UseSnakeCaseNamingConvention();


    }
}