namespace BusReservation.Api.DTOs;

public record UpdatePassengerDto(
    string FullName,
    string PhoneNumber,
    bool IsActive);