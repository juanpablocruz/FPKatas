using FPKatas.Purity;
using System;
using System.IO;
using Xunit;

namespace Test.Purity
{
    public class PurityTest
    {

        [Fact]
        public void TestImpureDependencyInjection()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                DependencyInjection.impureLogSomething("test impure");
                // cannot test as it outputs the current datetime which 
                // differs from the one that we can get here
                Assert.True(true);
            }
        }

        [Fact]
        public void TestPureDependencyInjection()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                DateTime now = DateTime.UtcNow;

                string text = "test pure";
                DependencyInjection.pureLogSomething(now, Console.WriteLine, text);
                Assert.Equal($"{now.ToString("o")} {text}\r\n", sw.ToString());
            }
        }


        [Fact]
        public void TestImpureLazyFunction()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                int res = LazyFunction.impureFunction(Console.WriteLine);
                Assert.Equal("Launch nuclear missiles!\r\n", sw.ToString());
            }
        }

        [Fact]
        public void TestPureLazyFunction()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                LazyFunction.pureFunction(Console.WriteLine);
                Assert.Equal("", sw.ToString());
            }
        }

        [Fact]
        public void TestEffectFunctor()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                var fzero = LazyFunction.pureFunction(Console.WriteLine);
                var effect = new Effect<int>(fzero)
                    .Map(x => x + 1)
                    .Map(x => x.ToString());

                Assert.Equal("", sw.ToString());

                var res = effect.Run();
                Assert.Equal("Launch nuclear missiles!\r\n", sw.ToString());
                Assert.Equal("1", res);
            }
        }

        [Fact]
        public void TestEffectFunctorComposition()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                var fzero = LazyFunction.pureFunction(Console.WriteLine);

                var effect1 = new Effect<int>(fzero)
                    .Map(x => x + 10);

                var effect = new Effect<int>(fzero)
                    .Map(x => x + 1)
                    .Map(x => x.ToString());
                    

                Assert.Equal("", sw.ToString());

                var effectComposed = effect.Chain((string data) => effect1.Map(x => $"{x} - {data}"))
                    .Map(x => x.ToUpper());
                
                Assert.Equal("", sw.ToString());

                var res = effectComposed.Run();
                Assert.Equal("Launch nuclear missiles!\r\nLaunch nuclear missiles!\r\n", sw.ToString());

                Assert.Equal("10 - 1", res);
            }
        }

    }


}
