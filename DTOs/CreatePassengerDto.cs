using System.ComponentModel.DataAnnotations;

namespace BusReservation.Api.DTOs;

public record CreatePassengerDto{

    [Required]
    [RegularExpression(@"^P-\d{4}$",ErrorMessage ="Passenger code must follow format P-0000")]
   public required  string PassengerCode{get;init;}

   [Required]
   [MaxLength(40)]
   public required  string FullName{get;init;}
    [Required]
    [RegularExpression(
        @"^09\d{8}$",
        ErrorMessage = "Phone number must follow Ethiopian format 09XXXXXXXX."
    )]
   public required string PhoneNumber{get;init;}
}