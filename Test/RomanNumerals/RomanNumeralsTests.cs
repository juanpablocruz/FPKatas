using FPKatas.RomanNumerals;
using SGS.Framework.FunK;
using Xunit;

namespace Test.RomanNumeralsTests
{
    using static F;
    public class RomanNumeralsTests
    {
        [Theory]
        [InlineData("I", 1)]
        [InlineData("V", 5)]
        [InlineData("X", 10)]
        [InlineData("L", 50)]
        [InlineData("C", 100)]
        [InlineData("D", 500)]
        [InlineData("M", 1000)]
        public void ElementalSymbolsHaveCorrectValues(string symbol, int expected)
        {
            var actual = RomanNumerals.TryParseRoman(symbol);            
            Assert.Equal(Just(expected), actual);
        }

        [Theory]
        [InlineData("MDCCCLXXXXIII", 1893)]
        public void AcceptsMalformedRomanNumerals(string symbol, int expected)
        {
            var actual = RomanNumerals.TryParseRoman(symbol);
            Assert.Equal(Just(expected), actual);
        }

        [Fact]
        public void ParseWorks()
        {
            var original = "MDCCCXCIII";
            var actual = RomanNumerals.TryParseRoman(original);
            Assert.Equal(Just(1893), actual);

            var roman = RomanNumerals.ToRoman(1893);
            Assert.Equal(Just(original), roman);
        }

        
    }
}
