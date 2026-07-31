using BusReservation.Api.DTOs;
using BusReservation.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusReservation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController(IBookingService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bookings = await service.GetAllAsync();

        return Ok(bookings);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var booking = await service.GetByIdAsync(id);

        if (booking is null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Booking not found",
                Detail = $"Booking with id {id} was not found.",
                Status = StatusCodes.Status404NotFound
            });
        }

        return Ok(booking);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingDto dto)
    {
        if (!await service.PassengerExistsAsync(dto.PassengerId))
        {
            return NotFound(new ProblemDetails
            {
                Title = "Passenger not found",
                Detail = $"Passenger with id {dto.PassengerId} was not found.",
                Status = StatusCodes.Status404NotFound
            });
        }

        if (!await service.RouteExistsAsync(dto.BusRouteId))
        {
            return NotFound(new ProblemDetails
            {
                Title = "Route not found",
                Detail = $"Route with id {dto.BusRouteId} was not found.",
                Status = StatusCodes.Status404NotFound
            });
        }

        if (await service.BookingExistsAsync(dto.PassengerId, dto.BusRouteId))
        {
            return Conflict(new ProblemDetails
            {
                Title = "Booking already exists",
                Detail = "This passenger has already booked this route.",
                Status = StatusCodes.Status409Conflict
            });
        }

        if (await service.SeatAlreadyBookedAsync(dto.BusRouteId, dto.SeatNumber))
        {
            return Conflict(new ProblemDetails
            {
                Title = "Seat already booked",
                Detail = $"Seat '{dto.SeatNumber}' has already been booked.",
                Status = StatusCodes.Status409Conflict
            });
        }

        if (await service.RouteIsFullAsync(dto.BusRouteId))
        {
            return Conflict(new ProblemDetails
            {
                Title = "Route is full",
                Detail = "This route has reached its maximum capacity.",
                Status = StatusCodes.Status409Conflict
            });
        }

        var booking = await service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = booking.Id },
            booking);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateBookingDto dto)
    {
        var updated = await service.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Booking not found",
                Detail = $"Booking with id {id} was not found.",
                Status = StatusCodes.Status404NotFound
            });
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await service.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Booking not found",
                Detail = $"Booking with id {id} was not found.",
                Status = StatusCodes.Status404NotFound
            });
        }

        return NoContent();
    }
}