using Microsoft.EntityFrameworkCore;
using RO.DevTest.Domain.Entities;

public class DefaultContext : DbContext
{
    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração do relacionamento entre Sale e Product
        modelBuilder.Entity<Sale>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}