using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI
{
    public interface IClock
    {
        DateTime GetCurrentDateTime();
    }
}
