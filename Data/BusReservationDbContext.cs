using Microsoft.EntityFrameworkCore;
using BusReservation.Api.Entities;
namespace  BusReservation.Api.Data;
public class BusReservationDbContext(
    DbContextOptions<BusReservationDbContext> options)
    : DbContext(options){
  public DbSet<Passenger> passengers=>Set<Passenger>();
  public DbSet<BusRoute> busRoutes=>Set<BusRoute>();
  public DbSet<Booking> bookings=>Set<Booking>();      
    
}