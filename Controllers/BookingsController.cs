using Microsoft.AspNetCore.Mvc;
using BusReservation.Api.Services;

namespace BusReservation.Api.Controllers;
[ApiController]
[Route("api/bookings")]


public class BookingsController(IBookingService bookingService):ControllerBase
{
    [HttpGet]
 public async Task<IActionResult> GetAll()
    {
        var bookings=await bookingService.GetAllAsync();
        return Ok(bookings);
    }   
    [HttpGet("{id}")]
  public async Task<IActionResult> GetById(string id)
    {
        var booking=await bookingService.GetByIdAsync(id);
            if (booking is null)
        {
            return NotFound();
        }


        return Ok(booking);
    } 

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateBookingRequest request)
    {
          var booking = await bookingService.CreateAsync(
            request.PassengerId,
            request.RouteCode);


        return CreatedAtAction(
            nameof(GetById),
            new { id = booking.Id },
            booking); 
    }
     [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await bookingService.CancelAsync(id);

        return deleted
            ? NoContent()
            : NotFound();
    }
     

}
public record CreateBookingRequest(
    string PassengerId,
    string RouteCode);