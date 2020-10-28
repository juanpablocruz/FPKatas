using System;

namespace BookingAPI
{
    public class SystemClock : IClock
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
