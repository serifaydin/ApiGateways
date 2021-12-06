using Reservation.API.Infrustructure;
using Reservation.API.Models;
using System;

namespace Reservation.API.Services
{
    public class ReservationService : IReservationService
    {
        public ReservationDTO GetResByBkgNumber(int BkgNumber)
        {
            return new ReservationDTO
            {
                Id = 10,
                Amount = 30,
                BkgDate = DateTime.Now,
                CheckingDate = DateTime.Now,
                CheckOutDate = DateTime.Now,
                ReservationNumber = 20
            };
        }
    }
}