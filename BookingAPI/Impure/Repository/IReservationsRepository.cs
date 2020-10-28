using System;
using System.Collections.Generic;

namespace BookingAPI
{
    public interface IReservationsRepository
    {
        IEnumerable<Reservation> ReadReservations(DateTime date);
        void Create(Reservation reservation);
    }
}
