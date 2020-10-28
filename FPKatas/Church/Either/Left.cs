using System;

namespace FPKatas.Church
{
    public class Left<L,R> : IEither<L,R>
    {
        private readonly L left;

        public Left(L left)
        {
            this.left = left;
        }

        public T Match<T>(Func<L, T> onLeft, Func<R, T> onRight)
            => onLeft(left);
    }
}
