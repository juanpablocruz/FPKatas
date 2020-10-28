using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.Church
{
    public interface IMaybe<T>
    {
        TResult Match<TResult>(TResult nothing, Func<T, TResult> just);
    }
}
