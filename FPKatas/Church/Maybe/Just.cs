using System;

namespace FPKatas.Church
{
    public class Just<T> : IMaybe<T>
    {
        private readonly T value;

        public Just(T value)
        {
            this.value = value;
        }

        public TResult Match<TResult>(TResult nothing, Func<T, TResult> just)
            => just(value);
    }
}
