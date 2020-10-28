using System;

namespace FPKatas.Church
{
    public interface INaturalNumber
    {
        T Match<T>(T zero, Func<INaturalNumber, T> succ);
    }
}
