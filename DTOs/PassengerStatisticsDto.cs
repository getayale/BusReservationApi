namespace BusReservation.Api.DTOs;

public record PassengerStatisticsDto(
    int TotalPassengers,
    int ActivePassengers);