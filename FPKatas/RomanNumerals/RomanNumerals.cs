using FPKatas.Helpers;
using SGS.Framework.FunK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPKatas.RomanNumerals
{
    using static F;
    public class RomanNumerals
    {
        public static Maybe<int> TryParseRoman(string symbol)
        {
            Func<int, Maybe<int>> add(Maybe<int> acc) => x => acc.Map( m => m + x);

            Maybe<int> imp(Maybe<int> acc, List<string> list)
            {
                var (h1, tail1) = list;
                var (h2, tail) = tail1;

                return list.Count() == 0 ? acc
                        :(h1+h2) == "IX" ? imp(add(acc)(9), tail)
                        : (h1 + h2) == "IV" ? imp(add(acc)(4), tail)
                        : h1 == "I" ? imp(add(acc)(1), tail1)
                        : h1 == "V" ? imp(add(acc)(5), tail1)
                        : (h1 + h2) == "XC" ? imp(add(acc)(90), tail)
                        : (h1 + h2) == "XL" ? imp(add(acc)(40), tail)
                        : h1 == "X" ? imp(add(acc)(10), tail1)
                        : h1 == "L" ? imp(add(acc)(50), tail1)
                        : (h1 + h2) == "CM" ? imp(add(acc)(900), tail)
                        : (h1 + h2) == "CD" ? imp(add(acc)(400), tail)
                        : h1 == "C" ? imp(add(acc)(100), tail1)
                        : h1 == "D" ? imp(add(acc)(500), tail1)
                        : h1 == "M" ? imp(add(acc)(1000), tail1)
                        : Nothing;
            }

            List<char> data = new List<char>();
            data.AddRange(symbol);
            return imp(Just(0), data.Select(x => x.ToString()).ToList());
        }


        public static Maybe<string> ToRoman(int i)
        {
            IEnumerable<string> imp(IEnumerable<string> acc, int n)
            => (n >= 1000) ? imp(new[] { "M" }.Concat(acc), n - 1000)
                : (n >= 900) ? imp(new[] { "M", "C" }.Concat(acc), n - 900)
                : (n >= 500) ? imp(new[] { "D" }.Concat(acc), n - 500)
                : (n >= 400) ? imp(new[] { "D", "C" }.Concat(acc), n - 400)
                : (n >= 100) ? imp(new[] { "C" }.Concat(acc), n - 100)
                : (n >= 90) ? imp(new[] { "C", "X" }.Concat(acc), n - 90)
                : (n >= 50) ? imp(new[] { "L" }.Concat(acc), n - 50)
                : (n >= 40) ? imp(new[] { "L", "X" }.Concat(acc), n - 40)
                : (n >= 10) ? imp(new[] { "X" }.Concat(acc), n - 10)
                : (n >= 9) ? imp(new[] { "X", "I" }.Concat(acc), n - 9)
                : (n >= 5) ? imp(new[] { "V" }.Concat(acc), n - 5)
                : (n >= 4) ? imp(new[] { "V", "I" }.Concat(acc), n - 4)
                : (n >= 1) ? imp(new[] { "I" }.Concat(acc), n - 1)
                : acc;
            if (0 < i && i < 4000)
                return Just(string.Join("",imp(new[] { "" }, i).Reverse()));
            return Nothing;
        }

    }
}
