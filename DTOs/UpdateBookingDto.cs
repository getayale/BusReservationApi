using System.ComponentModel.DataAnnotations;

namespace BusReservation.Api.DTOs;

public record UpdateBookingDto
(
    [Required]
    [StringLength(10)]
    string SeatNumber
);