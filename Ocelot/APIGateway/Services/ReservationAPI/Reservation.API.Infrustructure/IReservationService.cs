using Reservation.API.Models;

namespace Reservation.API.Infrustructure
{
    public interface IReservationService
    {
        public ReservationDTO GetResByBkgNumber(int BkgNumber);
    }
}