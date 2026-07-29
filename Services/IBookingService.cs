namespace BusReservation.Api.Services;
 public interface IBookingService
{
    Task<BookingRecord>CreateAsync(string passengerId,string routeCode);
    Task<BookingRecord?>GetByIdAsync(string id);
    Task<IReadOnlyList<BookingRecord>> GetAllAsync();
    Task<bool>CancelAsync(string id);
}