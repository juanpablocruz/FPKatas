using FPKatas.Mixin;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Test.Mixin
{
    public class IcecreamShopTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {

            yield return new object[] {
                new Happycream(),
                "with chocolate with caramel with pecans"
            };
            yield return new object[] {
                new NoChoco(),
                "with caramel with pecans"
            };
            yield return new object[] {
                new CaramelCream(),
                "with caramel"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }


    public class IcecreamShopTest
    {
        [Theory]
        [ClassData(typeof(IcecreamShopTestData))]
        public void Test(IceCream iceCream, string expected)
        {
            Assert.Equal(expected, Shop.DescribeIceCream(iceCream));
        }
    }
}
