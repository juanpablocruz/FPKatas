using ArchivePicture;
using FPKatas.RoseTree;
using SGS.Framework.FunK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Test.RoseTreeTest
{
    using static F;
    public class RoseTreeTest
    {
        private static T Id<T>(T x) => x;

        private static IRoseTree<int, string> exampleTree =
                RoseTree.Node(42,
                    RoseTree.Node(1337,
                        new RoseLeaf<int, string>("foo"),
                        new RoseLeaf<int, string>("bar")),
                    RoseTree.Node(2112,
                        RoseTree.Node(90125,
                            new RoseLeaf<int, string>("baz"),
                            new RoseLeaf<int, string>("qux"),
                            new RoseLeaf<int, string>("quux")),
                        new RoseLeaf<int, string>("quuz")),
                    new RoseLeaf<int, string>("corge"));

        public static IEnumerable<object[]> BifunctorLawsData
        {

            get
            {
                yield return new[] { new RoseLeaf<int, string>("") };
                yield return new[] { new RoseLeaf<int, string>("foo") };
                yield return new[] { RoseTree.Node<int, string>(42) };
                yield return new[] { RoseTree.Node(42, new RoseLeaf<int, string>("bar")) };
                yield return new[] { exampleTree };

            }
        }

        [Theory, MemberData(nameof(BifunctorLawsData))]
        public void MapNodeObeysFirstFunctoryLaw(IRoseTree<int, string> t)
        {
            Assert.Equal(t, t.MapNode(Id));
        }

        [Theory, MemberData(nameof(BifunctorLawsData))]
        public void MapLeafObeysFirstFunctorLaw(IRoseTree<int, string> t)
        {
            Assert.Equal(t, t.MapLeaf(Id));
        }

        [Theory, MemberData(nameof(BifunctorLawsData))]
        public void MapBothObeysIdentityLaw(IRoseTree<int, string> t)
        {
            Assert.Equal(t, t.BiMap(Id, Id));
        }

        [Theory, MemberData(nameof(BifunctorLawsData))]
        public void ConsistencyLawHolds(IRoseTree<int, string> t)
        {
            DateTime f(int i) => new DateTime(i);
            bool g(string s) => string.IsNullOrWhiteSpace(s);

            Assert.Equal(t.BiMap(f, g), t.MapLeaf(g).MapNode(f));
            Assert.Equal(
                t.MapNode(f).MapLeaf(g),
                t.MapLeaf(g).MapNode(f));
        }

        [Theory, MemberData(nameof(BifunctorLawsData))]
        public void SecondFunctorLawHoldsForMapNode(IRoseTree<int, string> t)
        {
            char f(bool b) => b ? 'T' : 'F';
            bool g(int i) => i % 2 == 0;

            Assert.Equal(
                t.MapNode(x => f(g(x))),
                t.MapNode(g).MapNode(f));
        }


        [Theory, MemberData(nameof(BifunctorLawsData))]
        public void SecondFunctorLawHoldsForMapLeaf(IRoseTree<int, string> t)
        {
            bool f(int x) => x % 2 == 0;
            int g(string s) => s.Length;

            Assert.Equal(
                t.MapLeaf(x => f(g(x))),
                t.MapLeaf(g).MapLeaf(f));
        }

        [Theory, MemberData(nameof(BifunctorLawsData))]
        public void MapBothCompositionLawHolds(IRoseTree<int, string> t)
        {
            char f(bool b) => b ? 'T' : 'F';
            bool g(int x) => x % 2 == 0;
            bool h(int x) => x % 2 == 0;
            int i(string s) => s.Length;

            Assert.Equal(
                t.BiMap(x => f(g(x)), y => h(i(y))),
                t.BiMap(g, i).BiMap(f, h));
        }

        [Theory]
        [ClassData(typeof(FilterTreeTestData))]
        public void FilterTree(IRoseTree<string,int> source, Func<int,bool> func, Maybe<IRoseTree<string,int>> expected)
        {
            var actual = source.Filter(func);

            Assert.Equal(expected, actual);
        }

    }

    
}
