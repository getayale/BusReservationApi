using BusReservation.Api.DTOs;

namespace BusReservation.Api.Services;

public interface IPassengerService
{
    Task<IReadOnlyList<PassengerDto>> GetAllAsync(int page);

    Task<PassengerDto?> GetByIdAsync(int id);

    Task<IReadOnlyList<PassengerDto>> GetDeletedAsync();


    Task<bool> PassengerCodeExistsAsync(
        string passengerCode);


    Task<PassengerDto> CreateAsync(
        CreatePassengerDto dto);


    Task<bool> UpdateAsync(
        int id,
        UpdatePassengerDto dto);


    Task<bool> DeleteAsync(
        int id);


    Task<IReadOnlyList<RouteBookingSummaryDto>>
        GetTopRoutesAsync();


    Task<PassengerStatisticsDto>
        GetStatisticsAsync();


    Task<int> ArchiveInactivePassengersAsync();
}