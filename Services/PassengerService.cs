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

        var passengers = await context.passengers
            .OrderBy(p => p.FullName)              // Stable sorting
            .Skip((page - 1) * pageSize)           // Skip previous pages
            .Take(pageSize)                        // Take current page
            .ToListAsync();


        return passengers
            .Select(MapToDto)
            .ToList();
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


        await context.SaveChangesAsync();


        return true;
    }




    // DELETE PASSENGER
    public async Task<bool> DeleteAsync(int id)
    {

        var passenger = await context.passengers
            .FirstOrDefaultAsync(p => p.Id == id);


        if (passenger == null)
            return false;


        context.passengers.Remove(passenger);


        await context.SaveChangesAsync();


        return true;
    }




    // GROUP BY + COUNT
    // Top 5 routes by booking count
    public async Task<IReadOnlyList<RouteBookingSummaryDto>>
        GetTopRoutesAsync()
    {

        var result = await context.bookings

            .GroupBy(b => b.BusRoute.RouteCode)

            .Select(g => new RouteBookingSummaryDto(
                g.Key,
                g.Count()
            ))

            .OrderByDescending(x => x.TotalBookings)

            .Take(5)

            .ToListAsync();


        return result;
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
            passenger.IsActive
        );
    }
}