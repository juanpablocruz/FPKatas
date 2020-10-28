using System;

namespace FPKatas.Church
{
    public class Zero : INaturalNumber
    {
        public T Match<T>(T zero, Func<INaturalNumber, T> succ)
            => zero;
    }
}
