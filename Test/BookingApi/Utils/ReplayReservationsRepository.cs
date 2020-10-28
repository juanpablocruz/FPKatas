using BookingAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.BookingApi
{
    public class ReplayReservationsRepository : IReservationsRepository
    {
        private readonly IDictionary<DateTime, Queue<IEnumerable<Reservation>>> reads;

        public ReplayReservationsRepository(
            IDictionary<DateTime, Queue<IEnumerable<Reservation>>> reads)
        {
            this.reads = reads;
        }

        public void Create(Reservation reservation)
        {
        }

        public IEnumerable<Reservation> ReadReservations(DateTime date)
        {
            return reads[date].Dequeue();
        }
    }
}
