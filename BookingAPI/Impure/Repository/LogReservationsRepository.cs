using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI
{
    public class LogReservationsRepository : IReservationsRepository
    {
        public IReservationsRepository Inner { get; }
        public ScopedLog Log { get; }
        public LogReservationsRepository(
            IReservationsRepository inner,
            ScopedLog log)
        {
            Inner = inner;
            Log = log;
        }

        public void Create(Reservation reservation)
        {
            Log.Observe(
                new Interaction
                {
                    Operation = nameof(Create),
                    Input = new { reservation }
                });
            Inner.Create(reservation);
        }

        public IEnumerable<Reservation> ReadReservations(DateTime date)
        {
            var reservations = Inner.ReadReservations(date);
            Log.Observe(
                new Interaction
                {
                    Operation = nameof(ReadReservations),
                    Input = new { date },
                    Output = reservations
                });
            return reservations;
        }
    }
}
