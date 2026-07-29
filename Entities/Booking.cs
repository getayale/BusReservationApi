namespace BusReservation.Api.Entities;

public class Booking
{
  
    public int Id { get; set; }

    public int PassengerId { get; set; }

    
    public int BusRouteId { get; set; }

  
    public required string SeatNumber { get; set; }

   
    public DateTime BookedAt { get; set; } = DateTime.UtcNow;

  
    public Passenger Passenger { get; set; } = null!;

    public BusRoute BusRoute { get; set; } = null!;
}