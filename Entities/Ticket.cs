namespace BusReservation.Api.Entities;

public class Ticket
{
    // Primary key
    public int Id { get; set; }

  
    public required string TicketNumber { get; set; }

    
    public decimal Price { get; set; }

  
    public int BookingId { get; set; }

  
    public Booking Booking { get; set; } = null!;
}