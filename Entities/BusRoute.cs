namespace BusReservation.Api.Entities;

public class BusRoute
{
    // Primary key
    public int Id { get; set; }

    // Route identifier
    public required string RouteCode { get; set; }

    // Departure city
    public required string Departure { get; set; }

    // Destination city
    public required string Destination { get; set; }

    // Maximum passengers
    public int Capacity { get; set; }

    // One route can have many bookings
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}