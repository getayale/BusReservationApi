using Microsoft.EntityFrameworkCore;
using BusReservation.Api.Data;
using BusReservation.Api.DTOs;
using BusReservation.Api.Entities;

namespace BusReservation.Api.Services;

public class PassengerService(
    BusReservationDbContext context) : IPassengerService
{

    // GET ALL PASSENGERS WITH PAGINATION
   public async Task<IReadOnlyList<PassengerDto>> GetAllAsync(int page)
{
    const int pageSize = 20;

    return await context.passengers
        .AsNoTracking()
        .OrderBy(p => p.FullName)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .Select(p => new PassengerDto(
            p.Id,
            p.PassengerCode,
            p.FullName,
            p.PhoneNumber,
            p.IsActive,
            p.Bookings.Count
        ))
        .ToListAsync();
}



    // GET PASSENGER BY ID
    public async Task<PassengerDto?> GetByIdAsync(int id)
    {
        var passenger = await context.passengers
            .FirstOrDefaultAsync(p => p.Id == id);


        if (passenger == null)
            return null;


        return MapToDto(passenger);
    }

public async Task<bool> PassengerCodeExistsAsync(
    string passengerCode)
{
    return await context.passengers
        .AsNoTracking()
        .AnyAsync(p => p.PassengerCode == passengerCode);
}

    // CREATE PASSENGER
    public async Task<PassengerDto> CreateAsync(
        CreatePassengerDto dto)
    {

        var passenger = new Passenger
        {
            PassengerCode = dto.PassengerCode,
            FullName = dto.FullName,
            PhoneNumber = dto.PhoneNumber,
            IsActive = true
        };


        context.passengers.Add(passenger);

        await context.SaveChangesAsync();


        return MapToDto(passenger);
    }




    // UPDATE PASSENGER
   public async Task<bool> UpdateAsync(
    int id,
    UpdatePassengerDto dto)
{
    var passenger = await context.passengers
        .FirstOrDefaultAsync(p => p.Id == id);

    if (passenger == null)
        return false;

    passenger.FullName = dto.FullName;
    passenger.PhoneNumber = dto.PhoneNumber;
    passenger.IsActive = dto.IsActive;

    try
    {
        await context.SaveChangesAsync();
        return true;
    }
    catch (DbUpdateConcurrencyException)
    {
        throw new Exception(
            "This passenger was modified by another user. Please reload and try again.");
    }
}


    // DELETE PASSENGER
    public async Task<bool> DeleteAsync(int id)
    {

        var passenger = await context.passengers
            .FirstOrDefaultAsync(p => p.Id == id);


        if (passenger == null)
            return false;


       passenger.IsDeleted=true;


        await context.SaveChangesAsync();


        return true;
    }

public async Task<IReadOnlyList<PassengerDto>> GetDeletedAsync()
{
    return await context.passengers
        .IgnoreQueryFilters()
        .Where(p => p.IsDeleted)
        .AsNoTracking()
        .Select(p => new PassengerDto(
            p.Id,
            p.PassengerCode,
            p.FullName,
            p.PhoneNumber,
            p.IsActive,
            p.Bookings.Count
        ))
        .ToListAsync();
}


    // GROUP BY + COUNT
    // Top 5 routes by booking count
   public async Task<IReadOnlyList<RouteBookingSummaryDto>> GetTopRoutesAsync()
{
    var data = await context.bookings
        .GroupBy(b => b.BusRoute.RouteCode)
        .Select(g => new
        {
            RouteCode = g.Key,
            TotalBookings = g.Count()
        })
        .OrderByDescending(x => x.TotalBookings)
        .Take(5)
        .ToListAsync();

    return data
        .Select(x => new RouteBookingSummaryDto(
            x.RouteCode,
            x.TotalBookings))
        .ToList();
}



    // AGGREGATE FUNCTIONS
    // Count, Count with condition
    public async Task<PassengerStatisticsDto>
        GetStatisticsAsync()
    {

        var totalPassengers =
            await context.passengers.CountAsync();


        var activePassengers =
            await context.passengers
                .CountAsync(p => p.IsActive);



        return new PassengerStatisticsDto(
            totalPassengers,
            activePassengers
        );
    }




    // ENTITY -> DTO MAPPING
    private static PassengerDto MapToDto(
        Passenger passenger)
    {
        return new PassengerDto(
            passenger.Id,
            passenger.PassengerCode,
            passenger.FullName,
            passenger.PhoneNumber,
            passenger.IsActive,
            passenger.Bookings.Count
        );
    }
    public async Task<int> ArchiveInactivePassengersAsync()
{
    var affectedRows = await context.passengers
        .Where(p => !p.IsActive && !p.IsArchived)
        .ExecuteUpdateAsync(setters =>
            setters.SetProperty(
                p => p.IsArchived,
                true));

    return affectedRows;
}
}