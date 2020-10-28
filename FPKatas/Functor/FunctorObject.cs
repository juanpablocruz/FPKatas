using System;

namespace FPKatas.Functor
{
    public class FunctorObject<T> : IFunctor<T>
    {
        T Value { get; }

        public FunctorObject(T val)
        {
            Value = val;
        }

        public IFunctor<R> map<R>(Func<T, R> func)
            => new FunctorObject<R>(func(Value));
    }
}
