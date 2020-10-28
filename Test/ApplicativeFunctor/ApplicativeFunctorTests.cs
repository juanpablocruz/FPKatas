using FPKatas.ApplicativeFunctor;
using FPKatas.Helpers;
using FPKatas.RomanNumerals;
using SGS.Framework.FunK;
using System;
using Test.ApplicativeFunctorTest;
using Xunit;

namespace Test.ApplicativeFunctorTests
{
    using static F;
    public class ApplicativeFunctorTests
    {
        [Fact]
        public void tests()
        {
            Func<string, string, string, string, string, string, string> concat =
                (x, y, z, a, b, c) => x + y + z + a + b + c;

            var combinations = new[] { concat.Curry()}
                .Apply(new[] { "P", "p" })
                .Apply(new[] { "a", "4" })
                .Apply(new[] { "ssw" })
                .Apply(new[] { "o", "0" })
                .Apply(new[] { "rd" })
                .Apply(new[] { "", "!" })
                ;

            var expected = new[]
            {
                "Password", "Password!", "Passw0rd", "Passw0rd!", "P4ssword", "P4ssword!",
                "P4ssw0rd", "P4ssw0rd!", "password", "password!", "passw0rd", "passw0rd!",
                "p4ssword", "p4ssword!", "p4ssw0rd", "p4ssw0rd!"
            };
            Assert.Equal(expected, combinations);

        }

        [Theory]
        [ClassData(typeof(MaybeApplicativeTestData))]
        public void ApplicativeOperations(string roman1, string roman2, Maybe<int> expected)
        {
            Func<int, int, int> difference = (n, m) => n - m;
            
            var actual = Just(difference.Curry())
                .Apply(RomanNumerals.TryParseRoman(roman2))
                .Apply(RomanNumerals.TryParseRoman(roman1));

            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(MaybeApplicativeTestData))]
        public void ApplicativeOperationsQuery(string roman1, string roman2, Maybe<int> expected)
        {
            Func<int, int, int> difference = (n, m) => n - m;

            var actual = from m in RomanNumerals.TryParseRoman(roman2)
                         from s in RomanNumerals.TryParseRoman(roman1)
                         select m - s;

            Assert.Equal(expected, actual);
        }
    }


}
