using Microsoft.EntityFrameworkCore;
using BusReservation.Api.Entities;
namespace  BusReservation.Api.Data;
public class BusReservationDbContext(
    DbContextOptions<BusReservationDbContext> options)
    : DbContext(options){
  public DbSet<Passenger> passengers=>Set<Passenger>();
  public DbSet<BusRoute> busRoutes=>Set<BusRoute>();
  public DbSet<Booking> bookings=>Set<Booking>();


  protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfigurationsFromAssembly(
        typeof(BusReservationDbContext).Assembly);
}
  
 public override async Task<int> SaveChangesAsync(
    CancellationToken cancellationToken = default)
{
    foreach (var entry in ChangeTracker.Entries())
    {
        if ((entry.Entity is Passenger || entry.Entity is Booking) &&
            (entry.State == EntityState.Added ||
             entry.State == EntityState.Modified))
        {
            entry.Property("LastUpdated").CurrentValue = DateTime.UtcNow;
        }
    }

    return await base.SaveChangesAsync(cancellationToken);
}
    
}