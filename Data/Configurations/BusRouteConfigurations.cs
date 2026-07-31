
using BusReservation.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BusReservation.Api.Data.Configurations;

public class BusRouteConfiguration : IEntityTypeConfiguration<BusRoute>
{
    public void Configure(EntityTypeBuilder<BusRoute> builder)
    {
        builder.HasKey(r=>r.Id);

        builder.Property(r=>r.RouteCode).IsRequired().HasMaxLength(20);
        builder.HasIndex(r=>r.RouteCode).IsUnique();

        builder.Property(r=>r.Departure).IsRequired().HasMaxLength(20);
        builder.Property(r=>r.Destination).IsRequired().HasMaxLength(20);
        builder.Property(r=>r.MaxCapacity).IsRequired();

         builder.HasMany(r => r.Bookings).WithOne(b => b.BusRoute).HasForeignKey(b => b.BusRouteId).OnDelete(DeleteBehavior.Restrict);;
    }
}