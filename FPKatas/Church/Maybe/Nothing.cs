using System;

namespace FPKatas.Church
{
    public class Nothing<T> : IMaybe<T>
    {
        public TResult Match<TResult>(TResult nothing, Func<T, TResult> just)
            => nothing;
    }
}
