using Microsoft.Extensions.Logging;


namespace BusReservation.Api.Services;
public class BookingService : IBookingService
{
     private readonly Dictionary<string, BookingRecord> _store = new();
       private readonly ILogger<BookingService> _logger;
        public BookingService(ILogger<BookingService> logger)
    {
        _logger = logger;
    }

    public Task<BookingRecord> CreateAsync(string passengerId,string routeCode)
    {
        var existing=_store.Values.FirstOrDefault(b=>b.PassengerId==passengerId&&
        b.RouteCode==routeCode);
        if(existing is not null)
        {
              _logger.LogWarning(
                "Duplicate booking attempt {PassengerId} already booked {RouteCode} (Booking {BookingId})",
                passengerId,
                routeCode,
                existing.Id);

            return Task.FromResult(existing);
        }
        var id=Guid.NewGuid().ToString("N")[..7];
        var booking=new BookingRecord(
            id,
            passengerId,
            routeCode,
            DateTime.UtcNow
        );
        _store[id]=booking;
         _logger.LogInformation(
            "Created booking {BookingId} for passenger {PassengerId} on route {RouteCode}",
            id,
            passengerId,
            routeCode);

        return Task.FromResult(booking);
    }


    public Task<BookingRecord?>GetByIdAsync(string id)
    {
        _store.TryGetValue(id,out var booking);
        if(booking is null)
        {
              _logger.LogWarning(
                "Booking {BookingId} not found",
                id);
        }
         return Task.FromResult(booking);
    }

      public Task<IReadOnlyList<BookingRecord>> GetAllAsync()
    {
        IReadOnlyList<BookingRecord> bookings = _store.Values.ToList();

        return Task.FromResult(bookings);
    }
     public Task<bool> CancelAsync(string id)
    {
        var removed = _store.Remove(id);

        if (removed)
        {
            _logger.LogInformation(
                "Cancelled booking {BookingId}",
                id);
        }
        else
        {
            _logger.LogWarning(
                "Cancel failed. Booking {BookingId} not found",
                id);
        }

        return Task.FromResult(removed);
    }
}
public record BookingRecord(
    string Id,
    string PassengerId,
    string RouteCode,
    DateTime CreatedAt);