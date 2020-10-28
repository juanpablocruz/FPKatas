using BookingAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.BookingApi
{
    public class ReplayClock : IClock
    {
        private readonly Queue<DateTime> times;

        public ReplayClock(IEnumerable<DateTime> times)
        {
            this.times = new Queue<DateTime>(times);
        }

        public DateTime GetCurrentDateTime()
        {
            return times.Dequeue();
        }
    }
}
