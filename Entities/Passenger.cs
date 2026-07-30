namespace BusReservation.Api.Entities;

public class Passenger
{
    // Primary key
    public int Id { get; set; }

    // Human-readable passenger identifier
    public required string PassengerCode { get; set; }

    // Passenger full name
    public required string FullName { get; set; }

    // Phone number
    public required string PhoneNumber { get; set; }

    // Indicates whether the passenger account is active
    public bool IsActive { get; set; } = true;
     public uint Version { get; set; }

    // One passenger can have many bookings
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}