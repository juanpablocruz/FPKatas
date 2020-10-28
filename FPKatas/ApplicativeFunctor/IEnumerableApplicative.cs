using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPKatas.ApplicativeFunctor
{
    public static class IEnumerableApplicative
    {
        public static IEnumerable<TResult> Apply<T, TResult>(
            this IEnumerable<Func<T, TResult>> selectors,
            IEnumerable<T> source)
            => selectors.SelectMany(source.Select);
    }
}
