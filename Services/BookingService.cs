using Microsoft.EntityFrameworkCore;
using BusReservation.Api.Data;
using BusReservation.Api.DTOs;
using BusReservation.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BusReservation.Api.Services;



public class BookingService(BusReservationDbContext context)
    : IBookingService
{
    public async Task<IReadOnlyList<BookingDto>> GetAllAsync()
    {
        return await context.bookings
            .AsNoTracking()
            .Include(b => b.Passenger)
            .Include(b => b.BusRoute)
            .OrderByDescending(b => b.BookedAt)
            .Select(b => new BookingDto(
    b.Id,
    b.PassengerId,
    b.Passenger.FullName,
    b.BusRouteId,
    b.BusRoute.RouteCode,
    b.SeatNumber,
    b.BookedAt
))
            .ToListAsync();
    }

    public async Task<BookingDto?> GetByIdAsync(int id)
    {
        return await context.bookings
            .AsNoTracking()
            .Include(b => b.Passenger)
            .Include(b => b.BusRoute)
            .Where(b => b.Id == id)
            .Select(b => new BookingDto(
    b.Id,
    b.PassengerId,
    b.Passenger.FullName,
    b.BusRouteId,
    b.BusRoute.RouteCode,
    b.SeatNumber,
    b.BookedAt
))
            .FirstOrDefaultAsync();
    }

    public async Task<BookingDto> CreateAsync(CreateBookingDto dto)
    {
        var booking = new Booking
        {
            PassengerId = dto.PassengerId,
            BusRouteId = dto.BusRouteId,
            SeatNumber = dto.SeatNumber
        };

        context.bookings.Add(booking);

        await context.SaveChangesAsync();

        await context.Entry(booking)
            .Reference(b => b.Passenger)
            .LoadAsync();

        await context.Entry(booking)
            .Reference(b => b.BusRoute)
            .LoadAsync();

        return MapToDto(booking);
    }

    public async Task<bool> UpdateAsync(
        int id,
        UpdateBookingDto dto)
    {
        var booking = await context.bookings
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking is null)
            return false;

        booking.SeatNumber = dto.SeatNumber;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var booking = await context.bookings
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking is null)
            return false;

        context.bookings.Remove(booking);

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> BookingExistsAsync(
        int passengerId,
        int busRouteId)
    {
        return await context.bookings
            .AsNoTracking()
            .AnyAsync(b =>
                b.PassengerId == passengerId &&
                b.BusRouteId == busRouteId);
    }

    public async Task<bool> SeatAlreadyBookedAsync(
        int busRouteId,
        string seatNumber)
    {
        return await context.bookings
            .AsNoTracking()
            .AnyAsync(b =>
                b.BusRouteId == busRouteId &&
                b.SeatNumber == seatNumber);
    }

    public async Task<bool> RouteExistsAsync(int busRouteId)
    {
        return await context.busRoutes
            .AsNoTracking()
            .AnyAsync(r => r.Id == busRouteId);
    }

    public async Task<bool> PassengerExistsAsync(int passengerId)
    {
        return await context.passengers
            .AsNoTracking()
            .AnyAsync(p => p.Id == passengerId);
    }

    public async Task<bool> RouteIsFullAsync(int busRouteId)
    {
        var route = await context.busRoutes
            .AsNoTracking()
            .Select(r => new
            {
                r.Id,
                r.MaxCapacity
            })
            .FirstOrDefaultAsync(r => r.Id == busRouteId);

        if (route is null)
            return false;

        var bookingCount = await context.bookings
            .AsNoTracking()
            .CountAsync(b => b.BusRouteId == busRouteId);

        return bookingCount >= route.MaxCapacity;
    }

    private static BookingDto MapToDto(Booking booking)
    {
        return new BookingDto(
            booking.Id,
            booking.PassengerId,
            booking.Passenger.FullName,
            booking.BusRouteId,
            booking.BusRoute.RouteCode,
            booking.SeatNumber,
            booking.BookedAt
        );
    }
}