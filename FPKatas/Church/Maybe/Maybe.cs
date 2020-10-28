using System;

namespace FPKatas.Church
{
    public static class Maybe
    {
        public static IChurchBoolean IsNothing<T>(this IMaybe<T> m)
            => m.Match<IChurchBoolean>(
                nothing: new ChurchTrue(),
                just: _ => new ChurchFalse());

        public static IChurchBoolean IsJust<T>(this IMaybe<T> m)
            => m.Match<IChurchBoolean>(
                nothing: new ChurchFalse(),
                just: _ => new ChurchTrue());

        public static IMaybe<TResult> Select<T, TResult>(
            this IMaybe<T> source,
            Func<T, TResult> selector)
            => source.Match<IMaybe<TResult>>(
                nothing: new Nothing<TResult>(),
                just: x => new Just<TResult>(selector(x)));

        public static IMaybe<TResult> Map<T, TResult>(
            this IMaybe<T> source,
            Func<T, TResult> selector)
            => source.Select(selector);
    }
}
