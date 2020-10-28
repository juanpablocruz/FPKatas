using SGS.Framework.FunK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Test.ApplicativeFunctorTest
{
    using static F;
   public class MaybeApplicativeTestData : IEnumerable<object[]>
    {
        static Func<int, Func<int, bool>> gt = n => x => n < x;
        public IEnumerator<object[]> GetEnumerator()
        {

            yield return new object[] {
               "MDCCCXCIII", "MM", Just(107)
            };

            yield return new object[] {
               "M", "DC", Just(-400)
            };

            yield return new object[] {
               "b", "DC", Nothing
            };

            yield return new object[] {
               "M", "N", Nothing
            };

            yield return new object[] {
               "b", "n", Nothing
            };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
