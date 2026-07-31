namespace BusReservation.Api.DTOs;

public record BookingDto
(
    int Id,
    int PassengerId,
    string PassengerName,
    int BusRouteId,
    string RouteCode,
    string SeatNumber,
    DateTime BookedAt
);