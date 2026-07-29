namespace BusReservation.Api.Entities;

public class Schedule
{
    // Primary key
    public int Id { get; set; }

    // Departure time
    public DateTime DepartureTime { get; set; }

    // Arrival time
    public DateTime ArrivalTime { get; set; }

    // Foreign key
    public int BusRouteId { get; set; }

    // Related route
    public BusRoute BusRoute { get; set; } = null!;
}