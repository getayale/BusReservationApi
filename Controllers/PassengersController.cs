
using BusReservation.Api.Services;
using Microsoft.AspNetCore.Mvc;
using BusReservation.Api.Services;
using BusReservation.Api.DTOs;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace  BusReservation.Api.Controllers;
[ApiController]
[Route("api/passengers")]
public class PassengersController(IPassengerService passengerService) : ControllerBase
{
    [HttpGet]
 public async Task<IActionResult> GetAll([FromQuery]int page=1)
    {
        var passengers=await passengerService.GetAllAsync(page);
          return Ok(passengers);
        
    }   
    [HttpGet("{id}")]
  public async Task<IActionResult> GetById(int id)
    {
        var passenger=await passengerService.GetByIdAsync(id);
         if (passenger is null)
            return NotFound();

        return Ok(passenger);
    }  

  [HttpPost]
  public async Task<IActionResult> Create(CreatePassengerDto dto)
    {
        var passenger= await passengerService.CreateAsync(dto);
        return CreatedAtAction(
            nameof(GetById),
            new{id=passenger.Id},
            passenger
        );
        
    }  

    [HttpPut("{id:int}")]
 public async Task<IActionResult> Update(int id,UpdatePassengerDto dto)
    {
        var updated=await passengerService.UpdateAsync(id,dto);
        if (!updated)
           return NotFound();

        return NoContent();   
    } 

 [HttpDelete("{id:int}")]

 public async Task<IActionResult> Delete(int id)
    {
        var deleted=await passengerService.DeleteAsync(id);
        if(!deleted)
           return NotFound();
        return NoContent();   
    }   

    [HttpGet("statistics")]
    public async Task<IActionResult> Statistics()
    {
       var stats=await  passengerService.GetStatisticsAsync();
        return Ok(stats); 
    } 
    [HttpGet("top-routes")]
    public async Task<IActionResult> TopRoutes()
    {
        var routes=await passengerService.GetTopRoutesAsync();
           return Ok(routes);
    }
}