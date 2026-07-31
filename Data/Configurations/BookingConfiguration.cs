

using BusReservation.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusReservation.Api.Data.Configurations;

public class BookingConfiguration:IEntityTypeConfiguration<Booking>
{
    
    public void Configure(EntityTypeBuilder<Booking> builder)
    {

        builder.HasKey(b=>b.Id);

         builder.Property(b => b.SeatNumber).IsRequired().HasMaxLength(10);
         builder.Property(b=>b.BookedAt).IsRequired();
        

         builder.Property<DateTime>("LastUpdated");

        builder.HasOne(b => b.Passenger)
            .WithMany(p => p.Bookings)
            .HasForeignKey(b => b.PassengerId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.BusRoute)
            .WithMany(r => r.Bookings)
            .HasForeignKey(b => b.BusRouteId)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}