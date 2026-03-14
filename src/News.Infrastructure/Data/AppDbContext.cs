using Microsoft.EntityFrameworkCore;
using News.Domain.Entities;

namespace News.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Menu> Menus { get; set; }
    public DbSet<NewsList> News { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<NewsList>()
            .HasOne(n => n.Menu)
            .WithMany(m => m.NewsList)
            .HasForeignKey(n => n.MenuId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
