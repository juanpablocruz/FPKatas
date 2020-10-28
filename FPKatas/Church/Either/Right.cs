using System;

namespace FPKatas.Church
{
    public class Right<L, R> : IEither<L, R>
    {
        private readonly R right;

        public Right(R right)
        {
            this.right = right;
        }

        public T Match<T>(Func<L, T> onLeft, Func<R, T> onRight)
            => onRight(right);
    }
}
