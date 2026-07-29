using System.ComponentModel.DataAnnotations;

namespace BusReservation.Api.Options;


// Strongly typed configuration for payment gateway settings.
public class PaymentOptions
{
    // Payment gateway URL is required.
    [Required]
    public required string GatewayUrl { get; init; }


    // Allowed payment amount range.
    [Range(100, 100000)]
    public decimal MaxDepositBirr { get; init; }
}