namespace BusReservation.Api.DTOs;

public record PassengerDto(
    int Id,
    string PassengerCode,
    string FullName,
    string PhoneNumber,
    bool IsActive);