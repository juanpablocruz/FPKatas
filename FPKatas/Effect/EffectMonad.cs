using System;

namespace FPKatas.EffectExample
{
    public class Effect<T, R>
    {
        Func<T, R> f { get; }
        public Effect(Func<T, R> func)
        {
            f = func;
        }

        public Effect<T,R2> map<R2>(Func<R, R2> fn)
           => new Effect<T,R2>((x) => fn(f(x)));

        public R runEffects(T x)
            => f(x);
    }

    public class Effect<T>
    {
        Func<T> f { get; }
        public Effect(Func<T> func)
        {
            f = func;
        }

        public Effect<R> map<R>(Func<T, R> fn)
            => new Effect<R>(() => fn(f()));

        public T runEffects()
            => f();
    }

    public static class EffectExtension
    {
        public static Effect<T, R> effect<T, R>(Func<T, R> func)
            => new Effect<T, R>(func);
        public static Effect<T> effect<T>(Func<T> func)
            => new Effect<T>(func);
    }

}
