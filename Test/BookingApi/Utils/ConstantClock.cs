using BookingAPI;
using System;

namespace Test.BookingApi.Utils
{
    public class ConstantClock : IClock
    {
        private readonly DateTime dateTime;

        public ConstantClock(DateTime dateTime)
        {
            this.dateTime = dateTime;
        }

        public DateTime GetCurrentDateTime()
        {
            return dateTime;
        }
    }
}
