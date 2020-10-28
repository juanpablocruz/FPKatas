using FPKatas.RoseTree;
using SGS.Framework.FunK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Test.RoseTreeTest
{
    using static F;
    using static RoseTree;
    public class FilterTreeTestData : IEnumerable<object[]>
    {
        static Func<int, bool> falseFn = (_) => false;
        static Func<int, bool> trueFn = (_) => true;

        static Func<int, Func<int, bool>> gt = n => x => n < x;
        public IEnumerator<object[]> GetEnumerator()
        {

            yield return new object[] {
               Leaf<string,int>(1),
               falseFn,
               F.Nothing
            };

            yield return new object[] {
               Leaf<string,int>(1),
               trueFn,
               Just(Leaf<string,int>(1))
            };

            yield return new object[] {
               Node("a", Leaf<string,int>(1)),
               trueFn,
               Just(Node("a", Leaf<string,int>(1)))
            };

            yield return new object[] {
               Node("b", Leaf<string,int>(1)),
               falseFn,
               Just(Node<string, int>("b"))
            };

            yield return new object[] {
               Node("", Leaf<string,int>(1), Leaf<string,int>(2)),
               gt(1),
               Just(Node("", Leaf<string,int>(2)))
            };

            yield return new object[] {
               Node("", 
                Node("", Leaf<string,int>(1), Leaf<string,int>(4),  Leaf<string,int>(2)),
                Node("", Leaf<string,int>(3), Leaf<string,int>(5))
               ),
               gt(2),
               Just(Node("", 
                Node("", Leaf<string,int>(4)),
                Node("", Leaf<string,int>(3), Leaf<string,int>(5))
               ))
            };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
