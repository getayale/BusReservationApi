namespace BusReservation.Api.Exceptions;

public class BookingDatabaseException(string message)
    : Exception(message);