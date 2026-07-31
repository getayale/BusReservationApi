using BusReservation.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BusReservation.Api.Data.Configurations;
public class PassengerConfiguration:IEntityTypeConfiguration<Passenger>
{
    
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        builder.HasKey(p=>p.Id);

        builder.Property(p=>p.PassengerCode).IsRequired().HasMaxLength(20);
         builder.HasIndex(p => p.PassengerCode).IsUnique();

         builder.Property(p=>p.FullName).IsRequired().HasMaxLength(50);

         builder.Property(p=>p.PhoneNumber).IsRequired().HasMaxLength(20);
         builder.Property(p=>p.IsActive).IsRequired().HasDefaultValue(true);

            builder.HasQueryFilter(p => !p.IsDeleted);
             builder.Property<DateTime>("LastUpdated");
              builder.Property(p => p.Version)
            .IsRowVersion();

             //relationship

    builder.HasMany(p => p.Bookings).WithOne(b => b.Passenger).HasForeignKey(b => b.PassengerId);

        
    }
}