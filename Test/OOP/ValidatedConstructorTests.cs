using FPKatas.OOP;
using System.Linq;
using Xunit;

namespace Test.OOP
{
    public class ValidatedConstructorTests
    {
        [Theory]
        [InlineData("Peter", 19, true)]
        [InlineData("Peter", 10, false)]
        [InlineData("Peter", 90, false)]
        [InlineData(" ", 90, false)]
        [InlineData(" ", 19, false)]
        public void CreateValidatesInput(string name, int age, bool isJust)
        {
            var obj = ValidatedConstructor.Of(name, age);
            Assert.Equal(expected: isJust, actual: obj.IsJust());
        }

        [Theory]
        [InlineData("Peter", 19, 0)]
        [InlineData(" ", 19, 1)]
        [InlineData(" ", 9, 2)]
        public void CreateValidatesWithErrorList(string name, int age, int errorCount)
        {
            var obj = ValidatedConstructor.OfValidated(name, age);
            Assert.Equal(expected: errorCount, actual: obj.Errors.Count());
        }
    }
}
