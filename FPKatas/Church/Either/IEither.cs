using System;

namespace FPKatas.Church
{
    public interface IEither<L,R>
    {
        T Match<T>(Func<L, T> onLeft, Func<R, T> onRight);
    }
}
