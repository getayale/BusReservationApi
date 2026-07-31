using BusReservation.Api.DTOs;

namespace BusReservation.Api.Services;

public interface IBookingService
{
    // Read
    Task<IReadOnlyList<BookingDto>> GetAllAsync();

    Task<BookingDto?> GetByIdAsync(int id);

    // Create
    Task<BookingDto> CreateAsync(CreateBookingDto dto);

    // Update
    Task<bool> UpdateAsync(
        int id,
        UpdateBookingDto dto);

    // Delete
    Task<bool> DeleteAsync(int id);

    // Business Rule Checks

    // Has this passenger already booked this route?
    Task<bool> BookingExistsAsync(
        int passengerId,
        int busRouteId);

    // Is the seat already taken on this route?
    Task<bool> SeatAlreadyBookedAsync(
        int busRouteId,
        string seatNumber);

    // Does the passenger exist?
    Task<bool> PassengerExistsAsync(int passengerId);

    // Does the route exist?
    Task<bool> RouteExistsAsync(int busRouteId);

    // Has the route reached its capacity?
    Task<bool> RouteIsFullAsync(int busRouteId);
}