using Microsoft.EntityFrameworkCore;

namespace TransaccionApi;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Transaccion> Transacciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.Property(e => e.PrecioUnitario).HasPrecision(18, 2);
            entity.Property(e => e.PrecioTotal).HasPrecision(18, 2);
        });
    }
}
