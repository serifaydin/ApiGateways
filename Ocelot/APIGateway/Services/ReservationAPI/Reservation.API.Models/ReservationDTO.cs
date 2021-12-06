using System;

namespace Reservation.API.Models
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public int ReservationNumber { get; set; }
        public DateTime? CheckingDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public DateTime? BkgDate { get; set; }
        public double Amount { get; set; }

    }
}