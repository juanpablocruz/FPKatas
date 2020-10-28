using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.Church
{
    public static class ChurchBoolean
    {
        public static IChurchBoolean ToChurchBoolean(this bool b)
            => b? (IChurchBoolean) new ChurchTrue() : new ChurchFalse();

        public static bool ToBool(this IChurchBoolean b)
            => b.Match(true, false);
    }

    
}
