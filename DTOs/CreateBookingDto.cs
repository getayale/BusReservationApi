using System.ComponentModel.DataAnnotations;

namespace BusReservation.Api.DTOs;

public record CreateBookingDto
(
    [Required]
    int PassengerId,

    [Required]
    int BusRouteId,

    [Required]
    [StringLength(10)]
    string SeatNumber
);