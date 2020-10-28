using System;

namespace FPKatas.Church
{
    public class Successor : INaturalNumber
    {
        private readonly INaturalNumber predecessor;

        public Successor(INaturalNumber n)
        {
            this.predecessor = n;
        }

        public T Match<T>(T zero, Func<INaturalNumber,T> succ)
            => succ(predecessor);
    }
}
