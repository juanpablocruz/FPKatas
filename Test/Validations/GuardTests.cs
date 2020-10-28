using FPKatas.Validations;
using SGS.Framework.FunK;
using Xunit;

namespace Test.GuardTests
{
    using static F;
    using static Guard;
    public static class GuardTests
    {
        [Fact]
        public static void CheckNullReturnsJust()
        {
            var validated = NotNull<int>(2);
            Assert.Equal(expected: Just(2), actual: validated);
        }

        [Fact]
        public static void CheckNullReturnsNothing()
        {
            var validated = NotNull<int>(null);
            Assert.Equal(expected: Nothing, actual: validated);
        }

        [Fact]
        public static void ValidationReturnsValid()
        {
            var validated = Validate(2, x => x < 3);
            Assert.Empty(validated.Errors);
        }

        [Fact]
        public static void ValidationReturnsErrors()
        {
            var validated = Validate(2, x => x < 1);
            Assert.Single(validated.Errors);
        }
    }
}
