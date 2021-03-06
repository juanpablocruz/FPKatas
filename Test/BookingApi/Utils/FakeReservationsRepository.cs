﻿using BookingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.BookingApi.Utils
{
    public class FakeReservationsRepository :
        Dictionary<int, Reservation>, IReservationsRepository
    {
        private int nextKey;

        public void Create(Reservation reservation)
        {
            // Not thread-safe...
            var key = ++nextKey;
            Add(key, reservation);
        }

        public IEnumerable<Reservation> ReadReservations(DateTime date)
        {
            var min = date.Date;
            var max = date.Date.AddDays(1).AddTicks(-1);

            return Values
                .Where(r => min <= r.Date && r.Date <= max)
                .ToArray();
        }
    }
}
