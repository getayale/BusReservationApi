namespace BusReservation.Api.DTOs;

public record RouteBookingSummaryDto(
    string RouteCode,
    int TotalBookings);