namespace BusReservation.Api.DTOs;

public record CreatePassengerDto(
    string PassengerCode,
    string FullName,
    string PhoneNumber);